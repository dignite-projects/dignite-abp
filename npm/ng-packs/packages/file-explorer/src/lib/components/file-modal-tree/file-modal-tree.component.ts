/* eslint-disable @typescript-eslint/no-this-alias */
/* eslint-disable @angular-eslint/component-selector */
import {
  AfterContentInit,
  Component,
  ElementRef,
  EventEmitter,
  inject,
  Input,
  Output,
  ViewChild,
} from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NzFormatEmitEvent, NzTreeNode } from 'ng-zorro-antd/tree';
import * as DescriptorService from '../../proxy/dignite/file-explorer/directories';
import { Confirmation, ConfirmationService, ToasterService } from '@abp/ng.theme.shared';
import { NzFormatBeforeDropEvent } from 'ng-zorro-antd/tree';
import { finalize, map, of, tap } from 'rxjs';
import { LocalizationService } from '@abp/ng.core';
import { ValidatorsService } from '@dignite-ng/expand.core';
let that;
@Component({
  selector: 'fe-file-modal-tree',
  templateUrl: './file-modal-tree.component.html',
  styleUrls: ['./file-modal-tree.component.scss'],
})
export class FileModalTreeComponent implements AfterContentInit {
  constructor(
    private _DescriptorService: DescriptorService.FileDescriptorService,
    private fb: FormBuilder,
    private toaster: ToasterService,
    public _LocalizationService: LocalizationService,
    private confirmation: ConfirmationService,
  ) {
    that = this;
  }

  /**文件分组列表 */
  fileGroupList: any[] = [];

  /**选择的tree节点 */
  _theSelectedTreeNode: any = '';
  @Input()
  public set theSelectedTreeNode(v: any) {
    this._theSelectedTreeNode = v;

    if (v == '') {
      this.loadData();
    }
  }

  /**正在编辑的节点 */
  theNodeBeingEdited: any = '';

  /**已展开的节点 */
  anExpandedNode: any[] = [];

  /**图片容器 */
  _fileContainerName: string;

  /**tree节点选择回调 */
  @Output() nodeClick = new EventEmitter();
  /**获取数据后回调给file-modal */
  @Output() treeNodeData = new EventEmitter();
  /**查看所有文件回调函数，在file-modal中处理逻辑 */
  @Output() lookAllBtn = new EventEmitter();

  @Input()
  public set fileContainerName(v: string) {
    if (v) {
      this._fileContainerName = v;
      // this.loadData();
    }
  }
  ngAfterContentInit(): void {
    //Called after ngOnInit when the component's or directive's content has been initialized.
    //Add 'implements AfterContentInit' to the class.
    if (this._fileContainerName) {
      this.loadData();
    }
  }

  handleClick = event => event.stopPropagation();

  loadData() {
    if (this._fileContainerName) {
      this.getFileGroupList();
    }
  }

  /**获取文件分组 */
  getFileGroupList() {
    this._DescriptorService
      .getList({
        containerName: this._fileContainerName,
      })
      .subscribe(async res => {
        this.fileGroupList = await this.setTheValueOfTheNodeRecursively(res.items);
        this.treeNodeData.emit(this.fileGroupList);
      });
  }

  /**查看所有文件 */
  onLookAllBtn() {
    this.lookAllBtn.emit();
  }

  /**递归-将列表转化为父子级结构 */
  setTheValueOfTheNodeRecursively(array: any[], parentId: any = null, root?: any[]): any {
    const rootList = root || array;
    const result = array.filter(item => item.parentId === parentId);
    result.sort((a, b) => a.order - b.order);
    result.map((el: any) => {
      el.title = el.name;
      el.key = el.id;
      el.entity = el.id; /** 节点值 */
      el.expanded = this.anExpandedNode.includes(el.key);
      el.children.sort((a, b) => a.order - b.order);
      if (el.children.length > 0) {
        this.setTheValueOfTheNodeRecursively(el.children, el.id, rootList);
      }
    });
    return result;
  }

  /**tree-拖拽 */
  nzEvent(event: NzFormatEmitEvent): void {}

  /**tree-拖拽 -验证*/
  beforeDrop(arg: NzFormatBeforeDropEvent) {
    const { pos, dragNode, node } = arg;
    // 只处理有效的拖拽位置
    if (pos === 0 || pos === 1 || pos === -1) {
      // 根据不同的拖拽位置计算参数
      const parentId = pos === 0 ? node.key : node.parentNode?.key || '';

      let order;
      if (pos === 1) {
        order = node.origin.order + 1;
      } else if (pos === 0) {
        order = node.origin.children.length + 1;
      } else {
        // pos === -1
        order = Math.max(0, node.origin.order - 1);
      }
      // 统一处理移动逻辑
      return that._DescriptorService.move(dragNode.key, { parentId, order }).pipe(
        tap(() => that.getFileGroupList()),
        map(() => true)
      );
    }

    return of(false);
  }

  selectedNode: NzTreeNode[] = [];
  /**tree--选择节点 */
  activeNode(node: NzTreeNode) {
    if ((event as any)?.target?.localName == 'i') return;
    if (this._theSelectedTreeNode?.key == node.key) return;
    this.selectedNode = [node];
    this._theSelectedTreeNode = node;
    this.nodeClick.emit(node);
  }

  /**判断节点是否选中 */
  isNodeSelected = el => {
    return el.key === this._theSelectedTreeNode?.key;
  };

  /**点击展开树节点图标触发 */
  nzExpandChange(event) {
    let anExpandedNode = this.anExpandedNode;
    if (anExpandedNode.includes(event.node.key)) {
      anExpandedNode = anExpandedNode.filter(key => key !== event.node.key);
    } else {
      anExpandedNode.push(event.node.key);
    }
    this.anExpandedNode = anExpandedNode;
  }

  /**增加分组 */
  addDescriptorBtn(items: any = '', edit = false) {
    this.ModalDescriptorOpen = true;
    this.ModalDescriptorForm = this.fb.group({
      containerName: [this._fileContainerName || '', Validators.required],
      name: ['', Validators.required],
      parentId: [items?.key || ''],
    });
    /**编辑 */
    if (edit) {
      this.theNodeBeingEdited = items.origin;
      this.ModalDescriptorForm.patchValue({
        name: items.origin.name,
      });
    }
  }

  /**删除分组 */
  deleteDescriptorBtn(node) {
    this.confirmation
      .warn('', {
        key: '',
        defaultValue: this._LocalizationService.instant(`FileExplorer::DeletionConfirmationMessage`,node.title),
      })
      .subscribe((status: Confirmation.Status) => {
        if (status == 'confirm') {
          this._DescriptorService.delete(node.key).subscribe(res => {
            this.ModalDescriptorOpen = false;
            if (this.theNodeBeingEdited.key == node.key) this.theNodeBeingEdited = '';
            this.getFileGroupList();
          });
        }
      });
  }

  /**分组 */
  /**模态框-状态-是否打开 */
  ModalDescriptorOpen = false;

  /**模态框-descriptor-繁忙状态-用于确定模态的繁忙状态是否为真 */
  ModalDescriptorBusy = false;

  /**模态框-descriptor-表单 */
  ModalDescriptorForm: FormGroup | undefined;

  /**模态框-descriptor-表单--控件模板-动态赋值表单控件 */
  @ViewChild('ModalFormDescriptorSubmit', { static: false }) ModalFormDescriptorSubmit: ElementRef;

  /**模态框-descriptor-状态改变回调 */
  ModalDescriptorVisibleChange(event) {
    if (!event) {
      this.ModalDescriptorForm = undefined;
      this.theNodeBeingEdited = '';
      return;
    }
  }
  formValidation: any = '';
  private _ValidatorsService = inject(ValidatorsService);
  /**f分组模态框保存 */
  createOrEditSave() {
    const input = this.ModalDescriptorForm.value;
    this.formValidation = this._ValidatorsService.getFormValidationStatus(this.ModalDescriptorForm);
    if (this._ValidatorsService.isCheckForm(this.formValidation, 'FileExplorer')) return;
    if (!this.ModalDescriptorForm.valid) return;
    if (this.ModalDescriptorBusy) return;
    this.ModalDescriptorBusy = true;

    if (this.theNodeBeingEdited) {
      this._DescriptorService
        .update(this.theNodeBeingEdited.key, input)
        .pipe(
          finalize(() => {
            this.ModalDescriptorBusy = false;
          })
        )
        .subscribe(res => {
          this.ModalDescriptorOpen = false;
          this.ModalDescriptorVisibleChange(false);
          this.getFileGroupList();
        });
      return;
    }
    this._DescriptorService
      .create(input)
      .pipe(
        finalize(() => {
          this.ModalDescriptorBusy = false;
        })
      )
      .subscribe(res => {
        this.ModalDescriptorOpen = false;
        this.ModalDescriptorVisibleChange(false);
        this.getFileGroupList();
      });
  }
}
