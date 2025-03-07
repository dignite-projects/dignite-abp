import {  RoutesService } from '@abp/ng.core';
import { APP_INITIALIZER } from '@angular/core';
import { eRegionalizationManagementRouteNames } from '../enums/route-names';
import { SettingTabsService } from '@abp/ng.setting-management/config';
import { RegionalizationComponent } from '../components/regionalization/regionalization.component';





export const Regionalization_Management_Setting_ROUTE_PROVIDERS = [
  {
    provide: APP_INITIALIZER,
    useFactory: configureSettingRoutes,
    deps: [RoutesService, SettingTabsService],
    multi: true,
  },
];


export function configureSettingRoutes(routesService: RoutesService, settingTabs: SettingTabsService) {
  return () => {
    settingTabs.add([
      {
        name: eRegionalizationManagementRouteNames.Regionalization,
        requiredPolicy: 'RegionalizationManagement.ManageRegions',
        component: RegionalizationComponent, 
      },
    ]);
    
  };
}
