import { ToolbarAction } from '@abp/ng.theme.shared/extensions';
import { CreateFieldComponent, EditFieldComponent, FieldViewComponent, FieldsComponent } from '../component';


/**字段 */
export const Fields_Defaults_Toolbar_Action = ToolbarAction.createMany<any[]>([
    {
        text: 'Cms::New',
        action: data => {
            const component = data.getInjected(FieldsComponent);
            component.toFieldsCreateBtn();
        },
        btnClass: '',
        permission: '',
        icon: 'fa fa-plus',
    },
]);
/**字段-创建 */
export const Fields_Create_Defaults_Toolbar_Action = ToolbarAction.createMany<any[]>([
    {
        text: 'AbpUi::Save',
        action: data => {
            const component = data.getInjected(CreateFieldComponent);
            component.submitclickBtn();
        },
        btnClass: '',
        permission: '',
        icon: 'fa fa-plus',
    },
]);
/**字段-编辑 */
export const Fields_Edit_Defaults_Toolbar_Action = ToolbarAction.createMany<any[]>([
    {
        text: 'AbpUi::Save',
        action: data => {
            const component = data.getInjected(EditFieldComponent);
            component.submitclickBtn();
        },
        btnClass: '',
        permission: '',
        icon: 'fa fa-plus',
    },
]);
/**字段-表单控件 */
export const form_contorl_Defaults_Toolbar_Action = ToolbarAction.createMany<any[]>([
    {
        text: 'AbpUi::Save',
        action: data => {
            const component = data.getInjected(FieldViewComponent);
            component.clickSubmit();
        },
        btnClass: '',
        permission: '',
        icon: 'fa fa-plus',
    },
]);

