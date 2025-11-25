/* eslint-disable @angular-eslint/component-selector */
import {
  ChangeDetectorRef,
  Component,
  ElementRef,
  inject,
  Input,
  ViewChild,
  AfterViewChecked,
} from '@angular/core';
import {
  AbstractControl,
  FormArray,
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { TreeConfig } from './tree-config';
import { ToPinyinService } from '@dignite-ng/expand.core';
import { LocalizationService } from '@abp/ng.core';

@Component({
  selector: 'df-tree-config',
  templateUrl: './tree-config.component.html',
  styleUrls: ['./tree-config.component.scss'],
})
export class TreeConfigComponent implements AfterViewChecked {
  constructor(private fb: FormBuilder, private toPinyinService: ToPinyinService) {}
  /**表单控件类型 */
  _type: any;
  @Input()
  public set type(v: any) {
    this._type = v;
  }

  /**选择的表单信息 */
  _selected: any;
  @Input()
  public set selected(v: any) {
    this._selected = v;
  }

  /**表单实体 */
  formEntity: FormGroup | undefined;
  @Input()
  public set Entity(v: FormGroup) {
    this.formEntity = v;
    this.dataLoaded();
  }
  get formConfiguration() {
    return this.formEntity?.get('formConfiguration') as FormGroup;
  }
  get TreeOptions() {
    return this.formConfiguration?.controls['TreeView.Nodes'] as FormArray;
  }
  @ViewChild('submitclick', { static: true }) submitclick: ElementRef;
  private cdr = inject(ChangeDetectorRef);
  async dataLoaded() {
    if (this.formEntity && this._type) {
      await this.AfterInit();
    }
  }

  AfterInit() {
    return new Promise(resolve => {
      this.formEntity?.setControl('formConfiguration', this.fb.group(new TreeConfig()));
      this.TreeOptions?.setValidators([Validators.required, Validators.minLength(1)]);
      this.cdr.detectChanges(); // 手动触发变更检测
      this.submitclick?.nativeElement?.click();
      if (this._selected && this._selected.formControlName == this._type) {
        const treeNodes = this._selected.formConfiguration['TreeView.Nodes'];
        if (treeNodes?.length) {
          this.nodes = this.convertTreeOptionsToNodes(treeNodes);
        }

        this.syncTreeOptionsFromNodes();
        // for (const element of this._selected.formConfiguration['TreeView.Nodes']) {
        //   for (const key in element) {
        //     const item = element[key];
        //     const capitalizedKey = key.charAt(0).toUpperCase() + key.slice(1);
        //     element[capitalizedKey] = item;
        //   }
        //   this.addTreeOptions();
        // }
        this.formConfiguration.patchValue({
          ...this._selected.formConfiguration,
        });
      } else {
        // this.addTreeOptions();
      }
      resolve(true);
    });
  }

  /**
   * 当选择项的文本发生变化时，更新对应的值
   * @param event 输入事件对象
   * @param index 选择项的索引位置
   * @description 如果选择项已有Value值则不处理，否则将中文文本转换为拼音作为Value值
   */
  // textChange(event, index) {
  //   const TreeOptionsItem = this.TreeOptions.at(index);
  //   const value = event.target.value;
  //   if (TreeOptionsItem.get('Value')?.value) return;
  //   TreeOptionsItem.patchValue({
  //     Value: structuredClone(value),
  //   });
  // }
  /**字段标签input失去标点生成字段名字 */
  disPlayNameInputBlur(event) {
    if (!this.nodeForm) return;
    const value = event.target.value;
    const pinyin = this.toPinyinService.get(value);
    const nameInput = this.nodeForm.get('key');
    if (!nameInput || nameInput.value) return;
    nameInput.patchValue(pinyin);
  }

  /**调整表格位置 */
  // drop(event: any) {
  //   moveItemInArray(this.TreeOptions.controls, event.previousIndex, event.currentIndex);
  //   this.TreeOptions.updateValueAndValidity();
  // }

  /**tree数据 */
  nodes: any[] = [];

  /**已展开的节点 */
  anExpandedNode: any[] = [];

  /**是否全部展开 */
  isAllExpanded = false;

  /**点击展开树节点图标触发 */
  nzExpandChange(event: any) {
    let anExpandedNode = this.anExpandedNode;
    if (anExpandedNode.includes(event.node.key)) {
      anExpandedNode = anExpandedNode.filter(key => key !== event.node.key);
    } else {
      anExpandedNode.push(event.node.key);
    }
    this.anExpandedNode = anExpandedNode;

    // 检查是否所有有子节点的节点都已展开
    // const allKeys = this.getAllNodeKeys(this.nodes);
    this.isAllExpanded = anExpandedNode.length > 0;
  }

  /**切换展开/收缩所有节点 */
  toggleExpandAll() {
    this.isAllExpanded = !this.isAllExpanded;
    this.anExpandedNode = this.isAllExpanded ? this.getAllNodeKeys(this.nodes) : [];
    if (!this.isAllExpanded) {
      this.nodes = [...this.setExpanded(this.nodes, false)];
    }
    this.cdr.detectChanges();
  }
  //递归设置nodes中的expanded值 并且返回一个数组
  setExpanded(nodes: any[], expanded: boolean): string[] {
    for (const node of nodes) {
      node.expanded = expanded;
      if (node.children?.length) {
        node.children = this.setExpanded(node.children, expanded);
      }
    }
    return nodes;
  }

  /**获取所有有子节点的节点的key */
  private getAllNodeKeys(nodes: any[]): string[] {
    const keys: string[] = [];
    for (const node of nodes) {
      if (node.children?.length) {
        keys.push(node.key);
        keys.push(...this.getAllNodeKeys(node.children));
      }
    }
    return keys;
  }

  /**检查是否有任何节点包含子节点 */
  hasAnyNodeWithChildren(): boolean {
    return this.getAllNodeKeys(this.nodes).length > 0;
  }

  /**更新展开状态 */
  private updateExpandedState() {
    const allKeys = this.getAllNodeKeys(this.nodes);
    this.isAllExpanded =
      allKeys.length > 0 && allKeys.every(key => this.anExpandedNode.includes(key));
  }
  /**正在操作的节点项 */
  selectTree: any;
  /**是否创建子节点 */
  isCreateChild: boolean | any = false;
  /**模态框状态 */
  isVisible: boolean | any = false;

  /**用于确定模态的繁忙状态是否为真 */
  modalBusy: boolean | any = false;
  /**创建表单 */
  nodeForm: FormGroup | undefined;

  /**表单控件模板-动态赋值表单控件 */
  @ViewChild('nodeModalSubmit', { static: false }) nodeModalSubmit: ElementRef;

  /**生成GUID */
  private generateGuid(): string {
    return crypto.randomUUID();
  }
  get keyInput() {
    return this.nodeForm?.get('key') as FormControl;
  }
  private _LocalizationService = inject(LocalizationService);
  SlugRegExValidator() {
    return (control: AbstractControl): { [key: string]: any } | null => {
      const regex = /^[a-zA-Z0-9_-]+$/;
      if (control.value && !regex.test(control.value)) {
        return { repetition: this._LocalizationService.instant(`Cms::SlugValidatorsText`) };
      }
      return null;
    };
  }

  /**创建节点 */
  addNodeBtn() {
    this.isVisible = true;
    this.isCreateChild = false;
    this.selectTree = null;
    this.nodeForm = this.fb.group({
      title: ['', Validators.required],
      key: ['', [Validators.required, this.SlugRegExValidator()]],
      isChecked: [false],
      children: new FormArray([]),
    });
  }
  /**编辑节点 */
  editItemBtn(node: any) {
    this.isVisible = true;
    this.isCreateChild = false;
    this.selectTree = node;
    this.nodeForm = this.fb.group({
      title: [node.title, Validators.required],
      key: [node.key, [Validators.required, this.SlugRegExValidator()]],
      isChecked: [node.origin?.isChecked ?? false],
      children: new FormArray([]),
    });
  }
  /**创建子节点 */
  createChildItemBtn(node: any) {
    this.isVisible = true;
    this.isCreateChild = true;
    this.selectTree = node;
    this.nodeForm = this.fb.group({
      title: ['', Validators.required],
      key: ['', [Validators.required, this.SlugRegExValidator()]],
      isChecked: [false],
      children: new FormArray([]),
    });
  }
  /**重置模态框 */
  resetModal() {
    this.isVisible = false;
    this.isCreateChild = false;
    this.selectTree = null;
  }
  /**创建编辑保存 */
  createOrEditSave() {
    const { value } = this.nodeForm;
    const { selectTree, isCreateChild, nodes, anExpandedNode } = this;
    const isMultiple = this.formConfiguration.controls['TreeView.Multiple'] as FormControl;

    if (selectTree) {
      if (isCreateChild) {
        // 创建子节点
        this.addChildNode(nodes, selectTree.key, value);
        // 自动展开父节点
        if (!anExpandedNode.includes(selectTree.key)) {
          this.anExpandedNode = [...anExpandedNode, selectTree.key];
        }
      } else {
        // 编辑节点
        this.updateNodeByReference(selectTree.origin, value);
      }
    } else {
      // 创建根节点
      nodes.push(value);
    }

    // 单选模式下，如果当前节点被选中，清除其他节点的选中状态
    if (!isMultiple.value && value.isChecked) {
      this.clearAllChecked(nodes);
      this.setNodeCheckedOnly(nodes, value.key, true);
    }

    this.nodes = [...nodes];
    this.syncTreeOptionsFromNodes();
    this.updateExpandedState();
    this.cdr.detectChanges();
    this.resetModal();
  }

  /**递归添加子节点 */
  private addChildNode(nodes: any[], parentKey: string, newNode: any): boolean {
    for (const node of nodes) {
      if (node.key === parentKey) {
        if (!node.children) node.children = [];
        node.children.push(newNode);
        return true;
      }
      if (node.children?.length && this.addChildNode(node.children, parentKey, newNode)) {
        return true;
      }
    }
    return false;
  }

  /**通过引用更新节点 */
  private updateNodeByReference(node: any, updatedData: any): void {
    node.title = updatedData.title;
    node.key = updatedData.key;
    node.isChecked = updatedData.isChecked;
  }

  /**删除节点 */
  deleteMenuItemBtn(node: any) {
    this.deleteNode(this.nodes, node.key);
    this.nodes = [...this.nodes];
    this.syncTreeOptionsFromNodes();
    this.updateExpandedState();
    this.cdr.detectChanges();
  }

  /**递归删除节点 */
  private deleteNode(nodes: any[], targetKey: string): boolean {
    for (let i = 0; i < nodes.length; i++) {
      if (nodes[i].key === targetKey) {
        nodes.splice(i, 1);
        return true;
      }
      if (nodes[i].children?.length && this.deleteNode(nodes[i].children, targetKey)) {
        return true;
      }
    }
    return false;
  }

  /**同步 nodes 到 TreeOptions */
  private syncTreeOptionsFromNodes() {
    this.TreeOptions.clear();
    const cleanedNodes = this.nodes.map(node => this.cleanNode(node));
    cleanedNodes.forEach(node => {
      this.TreeOptions.push(this.createTreeFormGroup(node));
    });
  }

  /**递归创建树表单组 */
  private createTreeFormGroup(node: any): FormGroup {
    const children = new FormArray([]);
    if (node.children?.length) {
      node.children.forEach(child => {
        children.push(this.createTreeFormGroup(child));
      });
    }
    return this.fb.group({
      Text: [node.title || '', Validators.required],
      Value: [node.key || '', Validators.required],
      Selected: [node.isChecked ?? false],
      Children: children,
    });
  }

  /**清理节点中的 ng-zorro 内部字段 */
  private cleanNode(node: any): any {
    const { title, key, isChecked, children } = node;
    return {
      title,
      key,
      isChecked,
      children: children?.length ? children.map(child => this.cleanNode(child)) : [],
    };
  }

  /**将 TreeOptions 数据结构转换为 nodes 数据结构 */
  private convertTreeOptionsToNodes(treeOptions: any[]): any[] {
    const result = [];
    for (const option of treeOptions) {
      result.push({
        title: option.Text,
        key: option.Value,
        isChecked: option.Selected ?? false,
        children: option.Children?.length ? this.convertTreeOptionsToNodes(option.Children) : [],
      });
    }
    return result;
  }

  /**设置 */
  toggleMultiple(event: any) {
    this.clearAllChecked(this.nodes);
    return;
    const isMultiple = this.formConfiguration.controls['TreeView.Multiple'] as FormControl;
    if (!isMultiple.value) {
      this.clearAllChecked(this.nodes);
    } else {
      this.applyMultipleLogic(this.nodes);
    }
    this.nodes = [...this.nodes];
    this.syncTreeOptionsFromNodes();
    this.cdr.detectChanges();
  }

  /**应用多选逻辑：选中的节点同时选中其所有子节点和父节点 */
  private applyMultipleLogic(nodes: any[]) {
    const checkedKeys: string[] = [];
    this.collectCheckedKeys(nodes, checkedKeys);
    checkedKeys.forEach(key => {
      this.setNodeChecked(nodes, key, true);
      this.setParentNodesChecked(nodes, key);
    });
  }

  /**收集所有选中节点的key */
  private collectCheckedKeys(nodes: any[], result: string[]) {
    for (const node of nodes) {
      if (node.isChecked) {
        result.push(node.key);
      }
      if (node.children?.length) {
        this.collectCheckedKeys(node.children, result);
      }
    }
  }

  /**切换节点选中状态 */
  toggleNodeChecked(event: any, node: any) {
    event.stopPropagation();
    const isMultiple = this.formConfiguration.controls['TreeView.Multiple'] as FormControl;
    const newChecked = !node.origin?.isChecked;

    if (!isMultiple.value) {
      this.clearAllChecked(this.nodes);
      if (newChecked) {
        this.setNodeCheckedOnly(this.nodes, node.key, true);
      }
    } else {
      this.setNodeChecked(this.nodes, node.key, newChecked);
      if (newChecked) {
        this.setParentNodesChecked(this.nodes, node.key);
      }
    }

    this.nodes = [...this.nodes];
    this.syncTreeOptionsFromNodes();
    this.cdr.detectChanges();
  }

  /**清除所有节点的选中状态 */
  private clearAllChecked(nodes: any[]) {
    for (const node of nodes) {
      node.isChecked = false;
      if (node.children?.length) {
        this.clearAllChecked(node.children);
      }
    }
  }

  /**设置指定节点的选中状态 */
  private setNodeChecked(nodes: any[], targetKey: string, checked: boolean): boolean {
    for (const node of nodes) {
      if (node.key === targetKey) {
        node.isChecked = checked;
        if (node.children?.length) {
          this.setChildrenChecked(node.children, checked);
        }
        return true;
      }
      if (node.children?.length && this.setNodeChecked(node.children, targetKey, checked)) {
        return true;
      }
    }
    return false;
  }

  /**递归设置所有子节点的选中状态 */
  private setChildrenChecked(children: any[], checked: boolean) {
    for (const child of children) {
      child.isChecked = checked;
      if (child.children?.length) {
        this.setChildrenChecked(child.children, checked);
      }
    }
  }

  /**选中所有父节点 */
  private setParentNodesChecked(nodes: any[], targetKey: string, parent: any = null): boolean {
    for (const node of nodes) {
      if (node.key === targetKey) {
        if (parent) {
          parent.isChecked = true;
          this.setParentNodesChecked(this.nodes, parent.key);
        }
        return true;
      }
      if (node.children?.length && this.setParentNodesChecked(node.children, targetKey, node)) {
        return true;
      }
    }
    return false;
  }

  /**仅设置指定节点的选中状态，不影响子节点和父节点 */
  private setNodeCheckedOnly(nodes: any[], targetKey: string, checked: boolean): boolean {
    for (const node of nodes) {
      if (node.key === targetKey) {
        node.isChecked = checked;
        return true;
      }
      if (node.children?.length && this.setNodeCheckedOnly(node.children, targetKey, checked)) {
        return true;
      }
    }
    return false;
  }

  /**拖拽 */
  dropOver(event: any) {
    const dragNode = event.dragNode;
    const targetNode = event.node;
    const pos = event.pos;

    // 移除被拖拽的节点
    const draggedData = this.removeNodeByKey(this.nodes, dragNode.key);
    if (!draggedData) return;

    // pos: 0=内部, -1=上方, 1=下方
    if (pos === 0) {
      // 放入目标节点内部作为子节点
      this.addChildNode(this.nodes, targetNode.key, draggedData);
      if (!this.anExpandedNode.includes(targetNode.key)) {
        this.anExpandedNode = [...this.anExpandedNode, targetNode.key];
      }
    } else {
      // 放在目标节点前后
      this.insertNodeBeside(this.nodes, targetNode.key, draggedData, pos);
    }

    this.nodes = [...this.nodes];
    this.syncTreeOptionsFromNodes();
    this.cdr.detectChanges();
  }

  /**移除并返回节点 */
  private removeNodeByKey(
    nodes: any[],
    targetKey: string,
    parent: any[] = null,
    index: number = -1
  ): any {
    for (let i = 0; i < nodes.length; i++) {
      if (nodes[i].key === targetKey) {
        return nodes.splice(i, 1)[0];
      }
      if (nodes[i].children?.length) {
        const found = this.removeNodeByKey(nodes[i].children, targetKey, nodes[i].children, i);
        if (found) return found;
      }
    }
    return null;
  }

  /**在目标节点旁边插入节点 */
  private insertNodeBeside(
    nodes: any[],
    targetKey: string,
    newNode: any,
    position: number
  ): boolean {
    for (let i = 0; i < nodes.length; i++) {
      if (nodes[i].key === targetKey) {
        const insertIndex = position === -1 ? i : i + 1;
        nodes.splice(insertIndex, 0, newNode);
        return true;
      }
      if (
        nodes[i].children?.length &&
        this.insertNodeBeside(nodes[i].children, targetKey, newNode, position)
      ) {
        return true;
      }
    }
    return false;
  }

  /**判断节点的子节点是否有被选中的 */
  hasChildrenChecked(node: any): boolean {
    if (node.origin?.isChecked) {
      return false;
    }
    const originNode = this.findNodeByKey(this.nodes, node.key);
    if (!originNode?.children?.length) {
      return false;
    }
    return this.hasAnyChildChecked(originNode.children);
  }

  /**递归检查子节点是否有被选中的 */
  private hasAnyChildChecked(children: any[]): boolean {
    for (const child of children) {
      if (child.isChecked) {
        return true;
      }
      if (child.children?.length && this.hasAnyChildChecked(child.children)) {
        return true;
      }
    }
    return false;
  }

  /**根据 key 查找节点 */
  private findNodeByKey(nodes: any[], key: string): any {
    for (const node of nodes) {
      if (node.key === key) {
        return node;
      }
      if (node.children?.length) {
        const found = this.findNodeByKey(node.children, key);
        if (found) return found;
      }
    }
    return null;
  }

  ngAfterViewChecked() {
    const checkboxes = document.querySelectorAll('input[type="checkbox"][data-indeterminate]');
    checkboxes.forEach((checkbox: HTMLInputElement) => {
      const indeterminate = checkbox.getAttribute('data-indeterminate') === 'true';
      checkbox.indeterminate = indeterminate;
    });
  }
}
