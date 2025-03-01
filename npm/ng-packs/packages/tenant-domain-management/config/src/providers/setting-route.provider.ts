import {  RoutesService } from '@abp/ng.core';
import { APP_INITIALIZER } from '@angular/core';
import { eTenancyDomainsManagementRouteNames } from '../enums/route-names';
import { SettingTabsService } from '@abp/ng.setting-management/config';
import { TenantDomainComponent } from '../components/tenant-domain/tenant-domain.component';





export const Tenancy_Domains_Management_Setting_ROUTE_PROVIDERS = [
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
        name: eTenancyDomainsManagementRouteNames.Domain,
        requiredPolicy: 'TenantDomainManagement.ManageDomain',
        component: TenantDomainComponent,
      },
    ]);
  };
}
