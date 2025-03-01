import {  RoutesService } from '@abp/ng.core';
import { APP_INITIALIZER } from '@angular/core';
import { SettingTabsService } from '@abp/ng.setting-management/config';





export const Tenancy_Domains_Management_ROUTE_PROVIDERS = [
  {
    provide: APP_INITIALIZER,
    useFactory: configureRoutes,
    deps: [RoutesService, SettingTabsService],
    multi: true,
  },
];


export function configureRoutes(routesService: RoutesService, settingTabs: SettingTabsService) {
  return () => {
  };
}
