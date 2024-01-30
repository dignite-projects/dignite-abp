/* eslint-disable @angular-eslint/no-empty-lifecycle-method */
/* eslint-disable @angular-eslint/use-lifecycle-interface */
/* eslint-disable @angular-eslint/component-selector */
import { ChangeDetectionStrategy, Component, EventEmitter, Input, Output, TemplateRef, ViewChild } from '@angular/core';
import { NzFormatEmitEvent, NzTreeNode } from 'ng-zorro-antd/tree';
import { NgbDropdown } from '@ng-bootstrap/ng-bootstrap';

import { delay, of } from 'rxjs';

import { NzFormatBeforeDropEvent } from 'ng-zorro-antd/tree';


var that
@Component({
  selector: 'dignite-tree',
  templateUrl: './tree.component.html',
  styleUrls: ['./tree.component.scss'],
  changeDetection: ChangeDetectionStrategy.Default,

})

export class TreeComponent {
  constructor() { }
  ngOnInit(): void {
    //Called after the constructor, initializing input properties, and the first call to ngOnChanges.
    //Add 'implements OnInit' to the class.
    that = this
  }

  /**上一次弹窗key */
  lastKey: string = ''

  /** 右键弹窗数据 */
  dropdowns = {} as { [key: string]: NgbDropdown };

  /**tree数据 */
  _nodes: any[] = []
  @Input()
  public set nodes(val: any[]) {
    this._nodes = this.setExpanded(val);
  };

  setExpanded(array: any[]) {
    array.forEach(el => {
      el.expanded = this.expandedKeys.includes(el.id)
      if (el.children.length > 0) {
        el.children = this.setExpanded(el.children)
      }
    })
    return array
  }
  /**选择的节点 */
  @Input() selectedNode: any;

  /**节点菜单 */
  @Input() nodeMenu: TemplateRef<any>

  /** */
  @Input() expandedKeys: any[] = []
  @Output() readonly expandedKeysChange = new EventEmitter()
  @ViewChild('nztree') nztree?;
  /**父组件选择节点 */
  @Output() readonly nodeChange = new EventEmitter();

  /**父组件菜单选择 */
  @Output() readonly menuChange = new EventEmitter();

  /**父组件节点移动 */
  @Output() readonly dropChange = new EventEmitter();

  isNodeSelected = node => this.selectedNode?.id === node.key;

  dropNode: any = ''

  /**拖拽 */
  nzDropEvent(event: NzFormatEmitEvent): void {
  }
  beforeDrop(arg: NzFormatBeforeDropEvent) {
    if(arg.node.isExpanded){
      that.expandedKeys.push(arg.node.key)
      that.expandedKeysChange.emit(that.expandedKeys);
    }
    that.dropNode = arg
    if (arg.pos === 0 || arg.pos === 1) {
      that.dropChange.emit(arg)
      return of(true)
    } else {
      return of(false);
    }
  }

  /**选择节点 */
  onSelectedNodeChange(node: NzTreeNode) {
    this.selectedNode = node.origin;
    this.nodeChange.emit(node.origin)
  }

  /**初始化右键弹窗数据 */
  initDropdown(key: string, dropdown: NgbDropdown) {
    this.dropdowns[key] = dropdown;
  }

  /**右键触发弹窗 */
  ContextMenu(key) {
    if (this.lastKey) this.dropdowns[this.lastKey]?.close()
    this.dropdowns[key]?.toggle()
    this.lastKey = key
  }

  /**选择菜单 */
  selectMenu(node) {
    this.menuChange.emit({
      node: node.origin,
      key: ''
    })
  }

  /**点击展开树节点图标触发 */
  onExpandedKeysChange(event) {
    this.expandedKeys = [...event.keys];
    this.expandedKeysChange.emit(event.keys);
    // this.nzExpandChange.emit(event);
  }


}
