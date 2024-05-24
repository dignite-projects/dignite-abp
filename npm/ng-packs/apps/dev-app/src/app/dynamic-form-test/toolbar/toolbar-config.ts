import { ConfigStateService } from "@abp/ng.core";
import { ExtensionsService, getObjectExtensionEntitiesFromStore, mapEntitiesToContributors, mergeWithDefaultActions } from "@abp/ng.theme.shared/extensions";
import { InjectionToken, inject } from "@angular/core";
import { ResolveFn } from "@angular/router";
import { map, tap } from "rxjs";
import { DynamicComponent } from "../enums";
import { 
    Fields_Create_Defaults_Toolbar_Action,
    Fields_Defaults_Toolbar_Action,
    Fields_Edit_Defaults_Toolbar_Action,
    form_contorl_Defaults_Toolbar_Action
 } from "./toolbar-btn-default-action";

export const ActionGroup = {
    FieldsCreate: Fields_Create_Defaults_Toolbar_Action,
    Fields: Fields_Defaults_Toolbar_Action,
    FieldsEdit: Fields_Edit_Defaults_Toolbar_Action,
    FieldsView: form_contorl_Defaults_Toolbar_Action,
};

const setDynamicAction = () => {
    let Dynamic_Default = {};
    for (const iterator in DynamicComponent) {
        Dynamic_Default[DynamicComponent[iterator]] = ActionGroup[iterator];
    }
    return Dynamic_Default;
};
/**用于路由守卫增加工具栏按钮 */
export const Dynamic_Default_Toolbar_Actions = setDynamicAction();

/**
 * 
 * @param 在identityExtensionsResolver方法中使用
 * 
 * @param 在Dynamic.module.ts中使用,
    export class DynamicModule {
        static forChild(options: any = {}): ModuleWithProviders<DynamicModule> {
            return {
                ngModule: DynamicModule,
                providers: [
                    {
                        provide: Dynamic_TOOLBAR_ACTION_CONTRIBUTORS,
                        useValue: options.toolbarActionContributors,
                        // useValue: identityToolbarActionContributors,
                    },
                    // IdentityExtensionsGuard,
                ]
            };
        }
        static forLazy(options: any = {}): NgModuleFactory<DynamicModule> {
            return new LazyModuleFactory(DynamicModule.forChild(options));
        }
    }
 * 
 * 
 */
export const Dynamic_TOOLBAR_ACTION_CONTRIBUTORS = new InjectionToken<any>(
    'Dynamic_TOOLBAR_ACTION_CONTRIBUTORS'
);

/**用于放在Dynamic-routing.module.ts路由守卫中
 * 
 *
    const routes: Routes = [
        {
            path: 'demo',
            canActivate: [AuthGuard, PermissionGuard],
            resolve: [identityExtensionsResolver],
            children: []
        },
    ]
 * 
 * 
 */
export const DynamicExtensionsResolver: ResolveFn<any> = () => {
    const configState = inject(ConfigStateService);
    const extensions = inject(ExtensionsService);
    const config = { optional: true };
    const toolbarContributors = inject(Dynamic_TOOLBAR_ACTION_CONTRIBUTORS, config) || {};
    return getObjectExtensionEntitiesFromStore(configState, 'Dynamic').pipe(
        map(entities => ({
            // [EDynamiceComponents.UsePage]: entities.Role,
            // [eIdentityComponents.Users]: entities.User,
        })),
        mapEntitiesToContributors(configState, 'Dynamic'),
        tap(objectExtensionContributors => {
            mergeWithDefaultActions(
                extensions.toolbarActions,
                Dynamic_Default_Toolbar_Actions,
                toolbarContributors
            );
        })
    );
};