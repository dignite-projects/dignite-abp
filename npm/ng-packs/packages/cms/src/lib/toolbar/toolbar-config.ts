import { ConfigStateService } from "@abp/ng.core";
import { ExtensionsService, getObjectExtensionEntitiesFromStore, mapEntitiesToContributors, mergeWithDefaultActions } from "@abp/ng.theme.shared/extensions";
import { InjectionToken, inject } from "@angular/core";
import { ResolveFn } from "@angular/router";
import { map, tap } from "rxjs";
import { ECmsComponent } from "../enums";
import { Entries_Defaults_Toolbar_Action, Entries_Create_Defaults_Toolbar_Action, Entries_Edit_Defaults_Toolbar_Action, Fields_Defaults_Toolbar_Action, Fields_Create_Defaults_Toolbar_Action, Fields_Edit_Defaults_Toolbar_Action, Sites_Defaults_Toolbar_Action, Sections_Defaults_Toolbar_Action, Sections_Create_Or_Edit_Defaults_Toolbar_Action } from "./toolbar-btn-default-action";

export const ActionGroup = {
    Entries: Entries_Defaults_Toolbar_Action,
    Entries_Create: Entries_Create_Defaults_Toolbar_Action,
    Entries_Edit: Entries_Edit_Defaults_Toolbar_Action,
    Fields: Fields_Defaults_Toolbar_Action,
    FieldsCreate: Fields_Create_Defaults_Toolbar_Action,
    FieldsEdit: Fields_Edit_Defaults_Toolbar_Action,
    Sites: Sites_Defaults_Toolbar_Action,
    Sections: Sections_Defaults_Toolbar_Action,
    SectionsCreateOrEdit: Sections_Create_Or_Edit_Defaults_Toolbar_Action,
};

const setCmsAction = () => {
    let Cms_Default = {};
    for (const iterator in ECmsComponent) {
        Cms_Default[ECmsComponent[iterator]] = ActionGroup[iterator];
    }
    return Cms_Default;
};
/**用于路由守卫增加工具栏按钮 */
export const Cms_Default_Toolbar_Actions = setCmsAction();

/**
 * 
 * @param 在identityExtensionsResolver方法中使用
 * 
 * @param 在cms.module.ts中使用,
    export class CmsModule {
        static forChild(options: any = {}): ModuleWithProviders<CmsModule> {
            return {
                ngModule: CmsModule,
                providers: [
                    {
                        provide: CMS_TOOLBAR_ACTION_CONTRIBUTORS,
                        useValue: options.toolbarActionContributors,
                        // useValue: identityToolbarActionContributors,
                    },
                    // IdentityExtensionsGuard,
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
export const CMS_TOOLBAR_ACTION_CONTRIBUTORS = new InjectionToken<any>(
    'CMS_TOOLBAR_ACTION_CONTRIBUTORS'
);

/**用于放在cms-routing.module.ts路由守卫中
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
export const CmsExtensionsResolver: ResolveFn<any> = () => {
    const configState = inject(ConfigStateService);
    const extensions = inject(ExtensionsService);
    const config = { optional: true };
    const toolbarContributors = inject(CMS_TOOLBAR_ACTION_CONTRIBUTORS, config) || {};
    return getObjectExtensionEntitiesFromStore(configState, 'Cms').pipe(
        map(entities => ({
            // [ECmseComponents.UsePage]: entities.Role,
            // [eIdentityComponents.Users]: entities.User,
        })),
        mapEntitiesToContributors(configState, 'Cms'),
        tap(objectExtensionContributors => {
            mergeWithDefaultActions(
                extensions.toolbarActions,
                Cms_Default_Toolbar_Actions,
                toolbarContributors
            );
        })
    );
};