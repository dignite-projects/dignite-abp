/* eslint-disable @angular-eslint/component-selector */
import { Component, Input, AfterContentInit } from '@angular/core';

@Component({
  selector: 'df-tree-view',
  templateUrl: './tree-view.component.html',
  styleUrls: ['./tree-view.component.scss'],
})
export class TreeViewComponent implements AfterContentInit {
 /**展示则内容 */
  showValue: any = '';

  /**是否显示再列表 */
  @Input() showInList = false;
  /**表单字段数据 */
  @Input() fields: any;

  /**表单控件类型 */
  @Input() type: any;

  /**表单控件Value */
  _value: any = '';
  @Input()
  public set value(v: any) {
    this._value = v;
  }

  async ngAfterContentInit(): Promise<void> {
    const valueOptions = this._value;
   
    if (this.type && valueOptions !== null && valueOptions !== undefined) {
      const nodes = this.fields?.field?.formConfiguration?.['TreeView.Nodes'] || [];
      const findNodeByValue = (nodeList: any[], val: any): any => {
        for (const node of nodeList) {
          if (node.value === val || node.Value === val) return node;
          if (node.children || node.Children) {
            const found = findNodeByValue(node.children || node.Children, val);
            if (found) return found;
          }
        }
        return null;
      };
      
      const getTextByValue = (val: any) => {
        const node = findNodeByValue(nodes, val);
        return node?.text || node?.Text || val;
      };
      
      if (Array.isArray(valueOptions)) {
        this.showValue = valueOptions.map(getTextByValue).join(',');
      } else {
        this.showValue = getTextByValue(valueOptions);
      }
    }
  }
}
