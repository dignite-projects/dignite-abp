import { ToolbarAction } from "@abp/ng.components/extensible";
import { MenuItemComponent } from "../components/menu-item/menu-item.component";
// import { PageComponent } from "../view/page/page.component";

/**
 * [eCmsKitRouteName.MenuItem]:MenuItem_Toolbar_Action
 */
export const MenuItem_Toolbar_Action = ToolbarAction.createMany<any[]>([
    {
        text: 'CmsKit::NewMenuItem',
        action: data => {
            const component = data.getInjected(MenuItemComponent);
            component.createMenuItemBtn();
        },
        btnClass: '',
        permission: 'CmsKit.Menus.Create',
        icon: 'fa fa-plus',
    },
]);

