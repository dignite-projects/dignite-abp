/* eslint-disable @angular-eslint/component-selector */
import { Component, inject, OnInit } from '@angular/core';
import {  eCmsKitRouteName } from '../../enums';
import { EXTENSIONS_IDENTIFIER } from '@abp/ng.components/extensible';
import { MenuItemAdminService } from '../../proxy/volo/cms-kit/admin/menus';
import { LocalizationService, PagedResultDto } from '@abp/ng.core';
import { MenuItemDto } from '../../proxy/volo/cms-kit/menus';
import { FormBuilder, FormGroup } from '@angular/forms';
import { menuItemFromConfig } from './menu-item-form-config';
import { ToasterService, ConfirmationService, Confirmation } from '@abp/ng.theme.shared';
import { UpdateListService } from '@dignite-ng/expand.core';

@Component({
  selector: 'cms-menu-item',
  templateUrl: './menu-item.component.html',
  styleUrl: './menu-item.component.scss',
  providers: [
    {
      provide: EXTENSIONS_IDENTIFIER,
      useValue: eCmsKitRouteName.MenuItem,
    },
  ],
})
export class MenuItemComponent implements OnInit {
  eCmsKitRouteName = eCmsKitRouteName;

  private _MenuItemAdminService = inject(MenuItemAdminService);
  private toaster = inject(ToasterService);
  private _LocalizationService = inject(LocalizationService);
  private _confirmationService = inject(ConfirmationService);
  private _UpdateListService = inject(UpdateListService);

  /**tree数据 */
  nodes: MenuItemDto[] = [];

  /**已展开的节点 */
  anExpandedNode: any[] = [];

  async ngOnInit(): Promise<void> {
    //Called after the constructor, initializing input properties, and the first call to ngOnChanges.
    //Add 'implements OnInit' to the class.
    await this.getMenuList();
    this._UpdateListService.updateListEvent.subscribe(async () => {
      await this.getMenuList();
    });
  }

  /**获取菜单 */
  getMenuList() {
    return new Promise((resolve, rejects) => {
      this._MenuItemAdminService.getList().subscribe(async res => {
        this.nodes = this.setListToNzNodes(res.items);
        resolve(res);
      });
    });
  }

  /**递归-将列表转化为父子级结构 */
  setListToNzNodes(array: any[], parentId: any = null, root?: any[]): any {
    let rootList = root || array;
    let result = array.filter(item => item.parentId === parentId);
    result.sort((a, b) => a.order - b.order);
    result.map((el: any) => {
      el.title = el.displayName;
      el.key = el.id;
      el.expanded = this.anExpandedNode.includes(el.key);
      el.children = rootList.filter(item => item.parentId === el.id);
      el.children.sort((a, b) => a.order - b.order);
      if (el.children.length > 0) {
        this.setListToNzNodes(el.children, el.id, rootList);
      }
    });
    return result;
  }

  /**点击展开树节点图标触发 */
  nzExpandChange(event: { node: { key: any } }) {
    let anExpandedNode = this.anExpandedNode;
    if (anExpandedNode.includes(event.node.key)) {
      anExpandedNode = anExpandedNode.filter(key => key !== event.node.key);
    } else {
      anExpandedNode.push(event.node.key);
    }
    this.anExpandedNode = anExpandedNode;
  }
  /**拖拽 */
  dropOver(event: any) {
    let message = '';
    let input = {
      position: 0,
      newParentId: '',
    };
    if (event.pos == 0) {
      input.position = event.pos;
      input.newParentId = event.node.key;
      message =this._LocalizationService.instant(`CmsKit::MenuItemMoveConfirmMessage`, event.dragNode.title, event.node.title);
    }
    if (event.pos == 1||event.pos == -1) {
      input.position = event.node.origin.order === 0 ? 0 : event.node.origin.order;
      input.newParentId = event.node.origin.parentId;
      message =this._LocalizationService.instant(`CmsKit::MenuItemMoveConfirmMessage`, event.dragNode.title, 'Root');
    }
 
    this._confirmationService
      .warn(`${message}`, this._LocalizationService.instant(`CmsKit::AreYouSure`), {})
      .subscribe(async (status: Confirmation.Status) => {
        if (status == 'confirm') {
          this._MenuItemAdminService.moveMenuItem(event.dragNode.key, input).subscribe(() => {
            this.toaster.success(this._LocalizationService.instant(`CmsKit::SavedSuccessfully`));
            this.getMenuList();
          });
        }
        if (status == 'reject') return;
      });
  }

  /**创建-编辑-新建子级-模态框 start */
  private fb = inject(FormBuilder);
  isVisible: boolean = false;

  visibleChange(event) {
    this.isVisible = event;
    this.menuItemForm = undefined;
    if (!event) {
      return;
    }
  }

  menuItemForm: FormGroup | undefined;
  /**创建菜单项 */
  createMenuItemBtn() {
    this.menuItemForm = this.fb.group(new menuItemFromConfig());
    this.isVisible = true;
  }
  /**编辑菜单项 */
  editMenuItemBtn(data: any) {
    this.menuItemForm = this.fb.group(new menuItemFromConfig());
    this.menuItemForm.patchValue({
      // parentId: data.key,
      ...data.origin,
    });
    this.isVisible = true;
  }
  /**创建菜单子项 */
  createChildMenuItemBtn(data: any) {
    this.menuItemForm = this.fb.group(new menuItemFromConfig());
    this.menuItemForm.patchValue({
      parentId: data.key,
    });
    this.isVisible = true;
  }
  /**删除菜单项 */
  deleteMenuItemBtn(data: any) {
    let input = data;
    // if (input.children.length > 0) {
    //   this.toaster.error(
    //     this._LocalizationService.instant(`CmsKit::${input.title}下含有子项，不允许删除`)
    //   );
    //   return;
    // }

    this._confirmationService
      .warn(
        `${input.title}`,
        this._LocalizationService.instant(
          `CmsKit::MenuItemDeletionConfirmationMessage`,
          input.title
        ),
        {}
      )
      .subscribe(async (status: Confirmation.Status) => {
        if (status == 'confirm') {
          this._MenuItemAdminService.delete(input.key).subscribe(() => {
            this.toaster.success(this._LocalizationService.instant(`CmsKit::SuccessfullyDeleted`));
            this.getMenuList();
          });
        }
        if (status == 'reject') return;
      });
  }

  /**创建-编辑-新建子级-模态框 end */
}
