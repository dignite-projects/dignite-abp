import { ModuleWithProviders, NgModule } from '@angular/core';
// import { Regionalization_Management_ROUTE_PROVIDERS } from './providers/route.provider';
import { Regionalization_Management_Setting_ROUTE_PROVIDERS } from './providers/setting-route.provider';
import { Regionalization_Management_ROUTE_PROVIDERS } from './providers';

@NgModule()
export class RegionalizationManagementConfigModule {
  static forRoot(): ModuleWithProviders<RegionalizationManagementConfigModule> {
    return {
      ngModule: RegionalizationManagementConfigModule,
      providers: [Regionalization_Management_Setting_ROUTE_PROVIDERS],
    };
  }
}
