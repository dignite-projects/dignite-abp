/* eslint-disable @angular-eslint/component-selector */
import { ChangeDetectorRef, Component, ElementRef, inject, Input, ViewChild, OnDestroy, AfterViewChecked } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'df-tree-control',
  templateUrl: './tree-control.component.html',
  styleUrls: ['./tree-control.component.scss'],
})
export class TreeControlComponent implements OnDestroy, AfterViewChecked {
  constructor(private fb: FormBuilder) {}

  /**字段配置列表 */
  _fields: any = '';
  @Input()
  public set fields(v: any) {
    this._fields = v;
  }

  /**父级字段名称，用于为表单设置控件赋值 */
  _parentFiledName: any;
  @Input()
  public set parentFiledName(v: any) {
    this._parentFiledName = v;
  }
  /**父级字段名称，用于为表单设置控件赋值 */
  _selected: any=[];
  @Input()
  public set selected(v: any) {
    this._selected = v||[];
  }

  /**表单实体 */
  _entity: FormGroup | undefined;
  @Input()
  public set entity(v: any) {
    this._entity = v;
    this.dataLoaded();
  }
  @ViewChild('submitclick', { static: true }) submitclick: ElementRef;

  get extraProperties() {
    return this._entity?.get('extraProperties') as FormGroup;
  }
  private cdr = inject(ChangeDetectorRef);
  /**数据加载完成 */
  async dataLoaded() {
    if (this._fields && this._entity) {
      await this.AfterInit();
      this.cdr.detectChanges(); // 手动触发变更检测
      this.submitclick?.nativeElement?.click();
    }
  }

  formConfiguration: any = '';
  AfterInit() {
    return new Promise((resolve, rejects) => {
      const ValidatorsArray: any[] = [];
      if (this._fields.required) {
        ValidatorsArray.push(Validators.required);
      }
      this.formConfiguration = this._fields.field.formConfiguration;
      const treeNodes = this.formConfiguration['TreeView.Nodes'];
      if (treeNodes?.length) {
        this.nodes = this.convertTreeOptionsToNodes(treeNodes);
      }
     
      // 如果没有预设值，从节点中获取默认选中的值
      if (!this._selected || this._selected.length === 0) {
        this._selected = this.getSelectedKeys(this.nodes);
      } else {
        // 有预设值时，同步更新 nodes 的选中状态
        this.syncNodesFromSelectedKeys(this._selected);
      }
      
      const newControl = this.fb.control(this._selected, ValidatorsArray);
      this.extraProperties.setControl(this._fields.field.name, newControl);
      resolve(true);
    });
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
  ngOnDestroy(): void {
    if (this.extraProperties && this._fields?.field?.name) {
      this.extraProperties.removeControl(this._fields.field.name);
    }
  }

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
    const allKeys = this.getAllNodeKeys(this.nodes);
    this.isAllExpanded = allKeys.length > 0 && allKeys.every(key => anExpandedNode.includes(key));
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

  /**递归设置nodes中的expanded值 并且返回一个数组 */
  private setExpanded(nodes: any[], expanded: boolean): any[] {
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

  /**切换节点选中状态 */
  toggleNodeChecked(event: any, node: any) {
    event.stopPropagation();
    const isMultiple = this.formConfiguration['TreeView.Multiple'];
    const newChecked = !node.origin?.isChecked;
    
    if (!isMultiple) {
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
    this.updateFormValue();
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

  /**更新表单值 */
  private updateFormValue() {
    const selectedKeys = this.getSelectedKeys(this.nodes);
    const control = this.extraProperties.get(this._fields.field.name);
    control?.setValue(selectedKeys);
  }

  /**获取所有选中节点的 key */
  private getSelectedKeys(nodes: any[]): string[] {
    const keys = [];
    for (const node of nodes) {
      if (node.isChecked) {
        keys.push(node.key);
      }
      if (node.children?.length) {
        keys.push(...this.getSelectedKeys(node.children));
      }
    }
    return keys;
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

  /**根据预设的 keys 同步更新 nodes 的选中状态 */
  private syncNodesFromSelectedKeys(selectedKeys: string[]) {
    this.clearAllChecked(this.nodes);
    for (const key of selectedKeys) {
      this.setNodeCheckedOnly(this.nodes, key, true);
    }
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

  ngAfterViewChecked() {
    const checkboxes = document.querySelectorAll('input[type="checkbox"][data-indeterminate]');
    checkboxes.forEach((checkbox: HTMLInputElement) => {
      const indeterminate = checkbox.getAttribute('data-indeterminate') === 'true';
      checkbox.indeterminate = indeterminate;
    });
  }
}
