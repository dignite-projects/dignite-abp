import { ToolbarAction } from '@abp/ng.theme.shared/extensions';
import {
    SectionsComponent,
    CreateOrEditComponent,
    SitesComponent,
    FieldsComponent,
    CreateFieldComponent,
    EditFieldComponent,
    EntriesComponent,
    CreateComponent,
    EditComponent,
} from '../components';

/**版块 */
export const Sections_Defaults_Toolbar_Action = ToolbarAction.createMany<any[]>([
    {
        text: 'Cms::New',
        action: data => {
            const component = data.getInjected(SectionsComponent);
            component.createSectionBtn();
        },
        btnClass: '',
        permission: 'CmsAdmin.Entry.Create',
        icon: 'fa fa-plus',
    },
]);
/**版块-创建-编辑 */
export const Sections_Create_Or_Edit_Defaults_Toolbar_Action = ToolbarAction.createMany<any[]>([
    {
        text: 'AbpUi::Save',
        action: data => {
            const component = data.getInjected(CreateOrEditComponent);
            component.submitclickBtn();
        },
        btnClass: '',
        permission: 'CmsAdmin.Entry.Create',
        icon: 'fas fa-save',
    },
]);
/**站点 */
export const Sites_Defaults_Toolbar_Action = ToolbarAction.createMany<any[]>([
    {
        text: 'Cms::New',
        action: data => {
            const component = data.getInjected(SitesComponent);
            component.createSitesBtn();
        },
        btnClass: '',
        permission: 'CmsAdmin.Entry.Create',
        icon: 'fa fa-plus',
    },
]);
/**字段 */
export const Fields_Defaults_Toolbar_Action = ToolbarAction.createMany<any[]>([
    {
        text: 'Cms::New',
        action: data => {
            const component = data.getInjected(FieldsComponent);
            component.toFieldsCreateBtn();
        },
        btnClass: '',
        permission: 'CmsAdmin.Entry.Create',
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
        permission: 'CmsAdmin.Entry.Create',
        icon: 'fa fa-plus',
    },
]);
/**字段-创建 */
export const Fields_Edit_Defaults_Toolbar_Action = ToolbarAction.createMany<any[]>([
    {
        text: 'AbpUi::Save',
        action: data => {
            const component = data.getInjected(EditFieldComponent);
            component.submitclickBtn();
        },
        btnClass: '',
        permission: 'CmsAdmin.Entry.Create',
        icon: 'fa fa-plus',
    },
]);

/**条目 */
export const Entries_Defaults_Toolbar_Action = ToolbarAction.createMany<any[]>([
    {
        text: 'Cms::New',
        action: data => {
            const component = data.getInjected(EntriesComponent);
            component.createEntriesBtn();
        },
        btnClass: '',
        permission: 'CmsAdmin.Entry.Create',
        icon: 'fa fa-plus',
    },
]);
/**条目-创建 */
export const Entries_Create_Defaults_Toolbar_Action = ToolbarAction.createMany<any[]>([
    {
        text: 'AbpUi::Cancel',
        action: data => {
            const component = data.getInjected(CreateComponent);
            component.backTo();
        },
        btnClass: 'btn btn-light btn-sm',
        permission: 'CmsAdmin.Entry.Create',
        icon: '',
    },
    {
        text: 'AbpUi::Save',
        action: data => {
            const component = data.getInjected(CreateComponent);
            component.clickSubmit(true);
        },
        btnClass: 'btn btn-info btn-sm',
        permission: 'CmsAdmin.Entry.Create',
        icon: 'fas fa-save',
    },
    {
        text: 'Cms::Publish',
        action: data => {
            const component = data.getInjected(CreateComponent);
            component.clickSubmit(false);
        },
        btnClass: '',
        permission: 'CmsAdmin.Entry.Create',
        icon: 'fa fa-paper-plane',
    },
]);
/**条目编辑 */
export const Entries_Edit_Defaults_Toolbar_Action = ToolbarAction.createMany<any[]>([
    {
        text: 'AbpUi::Cancel',
        action: data => {
            const component = data.getInjected(EditComponent);
            component.backTo();
        },
        btnClass: 'btn btn-light btn-sm',
        permission: 'CmsAdmin.Entry.Create',
        icon: '',
    },
    {
        text: 'AbpUi::Save',
        action: data => {
            const component = data.getInjected(EditComponent);
            component.clickSubmit(true);
        },
        btnClass: 'btn btn-info btn-sm',
        permission: 'CmsAdmin.Entry.Create',
        icon: 'fas fa-save',
    },
    {
        text: 'Cms::Publish',
        action: data => {
            const component = data.getInjected(EditComponent);
            component.clickSubmit(false);
        },
        btnClass: '',
        permission: 'CmsAdmin.Entry.Create',
        icon: 'fa fa-paper-plane',
    },
]);
