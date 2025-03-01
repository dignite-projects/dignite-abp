import { eLayoutType, RoutesService } from '@abp/ng.core';
import { APP_INITIALIZER } from '@angular/core';
import { eCmsRouteNames } from '../enums/route-names';
import { SettingTabsService } from '@abp/ng.setting-management/config';





export const CMS_ROUTE_PROVIDERS = [
  {
    provide: APP_INITIALIZER,
    useFactory: configureRoutes,
    deps: [RoutesService, SettingTabsService],
    multi: true,
  },
];


export function configureRoutes(routesService: RoutesService, settingTabs: SettingTabsService) {
  return () => {
    routesService.add([
      {
        path: '/cms',
        name: eCmsRouteNames.Cms,
        iconClass: 'bi-archive-fill',
        layout: eLayoutType.application,
        requiredPolicy: 'CmsAdmin.Entry || CmsAdmin.Field || CmsAdmin.Site || CmsAdmin.Section',
      },
      {
        path: '/cms/admin/entries',
        name: eCmsRouteNames.Entries,
        parentName: eCmsRouteNames.Cms,
        iconClass: 'bi-file-earmark-text-fill',
        layout: eLayoutType.application,
        requiredPolicy: 'CmsAdmin.Entry',
      },
      {
        path: '',
        name: eCmsRouteNames.Settings,
        parentName: eCmsRouteNames.Cms,
        iconClass: 'fas fa fa-cog',
        layout: eLayoutType.application,
        requiredPolicy: 'CmsAdmin.Field  || CmsAdmin.Section',
      },
      {
        path: '/cms/admin/fields',
        name: eCmsRouteNames.Fields,
        parentName: eCmsRouteNames.Settings,
        iconClass: 'bi-file-earmark-text-fill',
        layout: eLayoutType.application,
        order: 1,
        requiredPolicy: 'CmsAdmin.Field',
      },
      {
        path: '/cms/admin/sections',
        name: eCmsRouteNames.Sections,
        parentName: eCmsRouteNames.Settings,
        iconClass: 'bi bi-menu-button-wide-fill',
        layout: eLayoutType.application,
        order: 3,
        requiredPolicy: 'CmsAdmin.Section',
      },
      

    ]);
  };
}
