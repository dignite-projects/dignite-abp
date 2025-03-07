import { ModuleWithProviders, NgModule } from '@angular/core';
import { Regionalization_Management_Setting_ROUTE_PROVIDERS } from './providers/setting-route.provider';

@NgModule()
export class RegionalizationManagementConfigModule {
  static forRoot(): ModuleWithProviders<RegionalizationManagementConfigModule> {
    return {
      ngModule: RegionalizationManagementConfigModule,
      providers: [Regionalization_Management_Setting_ROUTE_PROVIDERS],
    };
  }
}
