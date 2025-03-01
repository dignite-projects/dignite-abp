import { eLayoutType, RoutesService } from '@abp/ng.core';
import { APP_INITIALIZER } from '@angular/core';
import { eRegionalizationManagementRouteNames } from '../enums/route-names';
import { SettingTabsService } from '@abp/ng.setting-management/config';
import { RegionalizationComponent } from '../components/regionalization/regionalization.component';





export const Regionalization_Management_ROUTE_PROVIDERS = [
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
