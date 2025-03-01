/* eslint-disable @angular-eslint/component-selector */
import { Component, ElementRef, EventEmitter, Input, Output, ViewChild, ViewContainerRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NzFormatEmitEvent } from 'ng-zorro-antd/tree';
import * as DescriptorService from '../../proxy/dignite/file-explorer/directories';
import {  ToasterService } from '@abp/ng.theme.shared';
import { NzFormatBeforeDropEvent } from 'ng-zorro-antd/tree';
import {  of } from 'rxjs';
import { FileApiService } from '../../services/file-api.service';
import { LocalizationService } from '@abp/ng.core';
var that;
@Component({
  selector: 'fe-file-modal-tree',
  templateUrl: './file-modal-tree.component.html',
  styleUrls: ['./file-modal-tree.component.scss']
})
export class FileModalTreeComponent {
  constructor(
    private _DescriptorService: DescriptorService.FileDescriptorService,
    private fb: FormBuilder,
    private toaster: ToasterService,
    public _FileApiService: FileApiService,
    public _LocalizationService: LocalizationService,
  ) {
    that = this
  }

  /**文件分组列表 */
  fileGroupList: any[] = [];

  /**选择的tree节点 */
  _theSelectedTreeNode: any = '';
  @Input()
  public set theSelectedTreeNode(v: any) {
    this._theSelectedTreeNode = v;
    if (v.length > 0) {
      this.loadData()
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


  @Input()
  public set fileContainerName(v: string) {
    if (v) {
      this._fileContainerName = v;
      this.loadData()
    }
  }

  handleClick = (event) => event.stopPropagation();

  loadData() {
    if (this._fileContainerName) {
      this.getFileGroupList()
    }
  }


  /**获取文件分组 */
  getFileGroupList() {
    this._DescriptorService.getList({
      containerName: this._fileContainerName,
    }).subscribe(async (res) => {
      this.fileGroupList = await this.setTheValueOfTheNodeRecursively(res.items)
    })
  }
  
  /**递归设置节点的值 */
  setTheValueOfTheNodeRecursively(array: any[]): any {
    return new Promise((reslove, rejects) => {
      array.forEach((el) => {
        el.title = el.name
        el.key = el.id
        el.expanded = this.anExpandedNode.includes(el.key)
        if (el.children.length > 0) {
          this.setTheValueOfTheNodeRecursively(el.children)
        }
      })
      reslove(array)
    })
  }

  /**tree-拖拽 */
  nzEvent(event: NzFormatEmitEvent): void {
  }

  /**tree-拖拽 -验证*/
  beforeDrop(arg: NzFormatBeforeDropEvent) {
    if (arg.pos === 0 || arg.pos === 1) {
      that._DescriptorService.move(arg.dragNode.key, {
        "parentId": arg.pos === 1 ? (arg.node.parentNode?.key||'') : arg.node.key,
        "order": arg.pos === 1 ? arg.node.origin.order + 1 : arg.node.origin.children.length + 1
      }).subscribe(res => {
        that.getFileGroupList()
      })
      return of(true)
    } else {
      return of(false);
    }
  }

  /**tree--选择节点 */
  activeNode(event) {
    this._theSelectedTreeNode = event.node
    this.nodeClick.emit(event.node)
  }

  /**判断节点是否选中 */
  isNodeSelected = (el) => el.key === this._theSelectedTreeNode?.key

  /**点击展开树节点图标触发 */
  nzExpandChange(event) {
    let anExpandedNode = this.anExpandedNode
    if (anExpandedNode.includes(event.node.key)) {
      anExpandedNode = anExpandedNode.filter(key => key !== event.node.key)
    } else {
      anExpandedNode.push(event.node.key)
    }
    this.anExpandedNode = anExpandedNode
  }


  /**增加分组 */
  addDescriptorBtn(items: any = '', edit = false) {
    this.ModalDescriptorOpen = true
    this.ModalDescriptorForm = this.fb.group({
      containerName: [this._fileContainerName || '', Validators.required],
      name: ['', Validators.required],
      parentId: [items?.key || '', Validators.required],
    })
    /**编辑 */
    if (edit) {
      this.theNodeBeingEdited = items.origin
      this.ModalDescriptorForm.patchValue({
        name: items.origin.name
      })
    }
  }

  /**删除分组 */
  deleteDescriptorBtn(node) {
    this._DescriptorService.delete(node.key).subscribe(res => {
      this.ModalDescriptorOpen = false
      if (this.theNodeBeingEdited.key == node.key) this.theNodeBeingEdited = ''
      this.getFileGroupList()
    })
  }

  

  /**分组 */
  /**模态框-状态-是否打开 */
  ModalDescriptorOpen: boolean = false

  /**模态框-descriptor-繁忙状态-用于确定模态的繁忙状态是否为真 */
  ModalDescriptorBusy: boolean = false

  /**模态框-descriptor-表单 */
  ModalDescriptorForm: FormGroup | undefined;
  
  /**模态框-descriptor-表单--控件模板-动态赋值表单控件 */
  @ViewChild('ModalFormDescriptorSubmit', { static: false }) ModalFormDescriptorSubmit: ElementRef;

  /**模态框-descriptor-状态改变回调 */
  ModalDescriptorVisibleChange(event) {
    if (!event) {
      this.ModalDescriptorForm = undefined
      this.theNodeBeingEdited = ''
      return
    }
  }

  /**f分组模态框保存 */
  createOrEditSave() {
    let input = this.ModalDescriptorForm.value
    if (this.theNodeBeingEdited) {
      this._DescriptorService.update(this.theNodeBeingEdited.key, input).subscribe(res => {
        this.ModalDescriptorOpen = false
        this.getFileGroupList()
      })
      return
    }
    this._DescriptorService.create(input).subscribe(res => {
      this.ModalDescriptorOpen = false
      this.getFileGroupList()
    })
  }




}
