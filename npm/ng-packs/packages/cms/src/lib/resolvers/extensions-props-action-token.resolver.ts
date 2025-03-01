import { ExtensionsService, getObjectExtensionEntitiesFromStore, mapEntitiesToContributors, mergeWithDefaultActions, mergeWithDefaultProps } from "@abp/ng.components/extensible";
import { ConfigStateService } from "@abp/ng.core";
import { inject, InjectionToken } from "@angular/core";
import { ResolveFn } from "@angular/router";
import { map, tap } from "rxjs";
import { ECmsComponent } from "../enums";
import { Entries_Create_Defaults_Toolbar_Action, Entries_Defaults_Toolbar_Action, Entries_Edit_Defaults_Toolbar_Action, Fields_Create_Defaults_Toolbar_Action, Fields_Defaults_Toolbar_Action, Fields_Edit_Defaults_Toolbar_Action, Sections_Create_Or_Edit_Defaults_Toolbar_Action, Sections_Defaults_Toolbar_Action } from "./page-default-toolbar-actions";
import {  Fields_Entity_Props } from "./table-default-entity-props";
import { Fields_Entity_Action } from "./table-default-entity-actions";






/**默认-实体-操作 */
export const Default_Entity_Actions = {
  // [ModuleComponent.AbpTable]: Abp_Table_Entity_Action,
  [ECmsComponent.Fields]: Fields_Entity_Action,
};

/**默认实体属性-表格列名 */
export const Default_Entity_Props = {
  // [PageName.Guides]: Guides_Entity_Props,
  [ECmsComponent.Fields]: Fields_Entity_Props,
};

/**默认-工具栏-操作 */
export const Default_Toolbar_Actions = {
  // [ModuleComponent.Page]: Page_Toolbar_Action,
  // [eCmsKitPageName.FaqQuestion]:FaqQuestion_Toolbar_Action
  [ECmsComponent.Entries]: Entries_Defaults_Toolbar_Action,
  [ECmsComponent.Entries_Create]: Entries_Create_Defaults_Toolbar_Action,
  [ECmsComponent.Entries_Edit]: Entries_Edit_Defaults_Toolbar_Action,
  [ECmsComponent.Fields]: Fields_Defaults_Toolbar_Action,
  [ECmsComponent.FieldsCreate]: Fields_Create_Defaults_Toolbar_Action,
  [ECmsComponent.FieldsEdit]: Fields_Edit_Defaults_Toolbar_Action,
  [ECmsComponent.Sections]: Sections_Defaults_Toolbar_Action,
  [ECmsComponent.SectionsCreateOrEdit]: Sections_Create_Or_Edit_Defaults_Toolbar_Action,
};


/**
 * 
 * @param 在Extensions_Props_Action_Token_Resolver方法中使用
 * @param 在.module.ts中使用,
    export class CmsModule {
        static forChild(options: any = {}): ModuleWithProviders<CmsModule> {
            return {
                ngModule: CmsModule,
                providers: [
                    {
                      provide: Toolbar_Action_Contributors,
                      useValue: options.toolbarActionContributors,
                    },
                    {
                      provide: Entity_Action_Contributors,
                      useValue: options.entityActionContributors,
                    },
                    {
                      provide: Entity_Props_Contributors,
                      useValue: options.entityPropContributors,
                    },
                ]
            };
        }
        static forLazy(options: any = {}): NgModuleFactory<CmsModule> {
            return new LazyModuleFactory(CmsModule.forChild(options));
        }
    }
 * 
 * 
 */
/**实体-操作-贡献者 */
export const Entity_Action_Contributors =
  new InjectionToken<any>('Entity_Action_Contributors');

/**实体-属性-贡献者 */
export const Entity_Props_Contributors =
  new InjectionToken<any>('Entity_Props_Contributors');

export const Toolbar_Action_Contributors = new InjectionToken<any>(
  'Toolbar_Action_Contributors'
);

/**
 * 属性-操作-贡献者-解析器
 * 
 * 用于放在-routing.module.ts路由守卫中
 * 
 *
    const routes: Routes = [
        {
            path: 'demo',
            canActivate: [AuthGuard, PermissionGuard],
            resolve: [Extensions_Props_Action_Token_Resolver],
            children: []
        },
    ]
 * 
 * 
 */

export const Extensions_Props_Action_Token_Resolver: ResolveFn<any> = () => {
  const configState = inject(ConfigStateService);
  const extensions = inject(ExtensionsService);

  const config = { optional: true };

  const actionContributors = inject(Entity_Action_Contributors, config) || {};
  const propContributors = inject(Entity_Props_Contributors, config) || {};
  const toolbarContributors = inject(Toolbar_Action_Contributors, config) || {};


  return getObjectExtensionEntitiesFromStore(configState, 'TenantManagement').pipe(
    map(entities => ({
      // [ModuleComponent.AbpTable]: entities.Tenant,
    })),
    mapEntitiesToContributors(configState, 'TenantManagement'),
    tap(objectExtensionContributors => {
      //actions
      mergeWithDefaultActions(
        extensions.entityActions,
        Default_Entity_Actions,
        actionContributors,
      );
      //props
      mergeWithDefaultProps(
        extensions.entityProps,
        Default_Entity_Props,
        objectExtensionContributors.prop,
        propContributors,
      );
      //Toolbar
      mergeWithDefaultActions(
        extensions.toolbarActions,
        Default_Toolbar_Actions,
        toolbarContributors
      );

    }),
  );
};


// /**
//  * @deprecated Use `tenantManagementExtensions_Props_Action_Token_Resolver` *function* instead.
//  */
// @Injectable()
// export class TenantManagementExtensionsGuard implements IAbpGuard {
//   protected readonly configState = inject(ConfigStateService);
//   protected readonly extensions = inject(ExtensionsService);

//   canActivate(): Observable<boolean> {
//     const config = { optional: true };

//     const actionContributors = inject(TENANT_MANAGEMENT_Entity_Action_Contributors, config) || {};
//     const propContributors = inject(TENANT_MANAGEMENT_Entity_Props_Contributors, config) || {};

//     return getObjectExtensionEntitiesFromStore(this.configState, 'TenantManagement').pipe(
//       map(entities => ({
//         // [eTenantManagementComponents.Tenants]: entities.Tenant,
//       })),
//       mapEntitiesToContributors(this.configState, 'TenantManagement'),
//       tap(objectExtensionContributors => {
//         mergeWithDefaultActions(
//           this.extensions.entityActions,
//           DEFAULT_TENANT_MANAGEMENT_ENTITY_ACTIONS,
//           actionContributors,
//         );
//         mergeWithDefaultProps(
//           this.extensions.entityProps,
//           DEFAULT_TENANT_MANAGEMENT_ENTITY_PROPS,
//           objectExtensionContributors.prop,
//           propContributors,
//         );
//       }),
//       map(() => true),
//     );
//   }
// }

