import { ModuleWithProviders, NgModule } from '@angular/core';
import { REGIONALIZATION_MANAGEMENT_ROUTE_PROVIDERS } from './providers/route.provider';

@NgModule()
export class RegionalizationManagementConfigModule {
  static forRoot(): ModuleWithProviders<RegionalizationManagementConfigModule> {
    return {
      ngModule: RegionalizationManagementConfigModule,
      providers: [REGIONALIZATION_MANAGEMENT_ROUTE_PROVIDERS],
    };
  }
}
