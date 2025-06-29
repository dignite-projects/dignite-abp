import { ToolbarAction } from '@abp/ng.components/extensible';
import {
  SectionsComponent,
  CreateOrEditComponent,
  FieldsComponent,
  CreateFieldComponent,
  EditFieldComponent,
  EntriesComponent,
  CreateComponent,
  EditComponent,
} from '../components';


/**
 * 字段列表-新建
 * [ECmsComponent.Fields]:Fields_Defaults_Toolbar_Action
 */
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

/**字段创建页-保存 */
export const Fields_Create_Defaults_Toolbar_Action = ToolbarAction.createMany<any[]>([
  {
    text: 'AbpUi::Save',
    action: data => {
      const component = data.getInjected(CreateFieldComponent);
      //   component.submitclickBtn();
      component.submitclick?.nativeElement?.click();
    },
    btnClass: '',
    permission: 'CmsAdmin.Entry.Create',
    icon: 'fas fa-save',
  },
]);
/**字段创建页-保存 */
export const Fields_Edit_Defaults_Toolbar_Action = ToolbarAction.createMany<any[]>([
  {
    text: 'AbpUi::Save',
    action: data => {
      const component = data.getInjected(EditFieldComponent);
      //   component.submitclickBtn();
      component.submitclick?.nativeElement?.click();
    },
    btnClass: '',
    permission: 'CmsAdmin.Entry.Create',
    icon: 'fas fa-save',
  },
]);

/**版块 */
export const Sections_Defaults_Toolbar_Action = ToolbarAction.createMany<any[]>([
  {
    text: 'Cms::New',
    action: data => {
      const component = data.getInjected(SectionsComponent);
      component.createBtn();
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
// export const Sites_Defaults_Toolbar_Action = ToolbarAction.createMany<any[]>([
//     {
//         text: 'Cms::New',
//         action: data => {
//             const component = data.getInjected(SitesComponent);
//             // component.createSitesBtn();
//         },
//         btnClass: '',
//         permission: 'CmsAdmin.Entry.Create',
//         icon: 'fa fa-plus',
//     },
// ]);


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
    text: 'Cms::SaveDraft',
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
    text: 'Cms::SaveDraft',
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
