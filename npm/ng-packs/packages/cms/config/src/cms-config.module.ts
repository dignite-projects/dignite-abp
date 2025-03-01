import { ModuleWithProviders, NgModule } from '@angular/core';
import { CMS_ROUTE_PROVIDERS } from './providers/route.provider';
import { Regionalization_Management_Setting_ROUTE_PROVIDERS } from '@dignite-ng/expand.regionalization-management/config';

@NgModule()
export class CmsConfigModule {
  static forRoot(): ModuleWithProviders<CmsConfigModule> {
    return {
      ngModule: CmsConfigModule,
      providers: [CMS_ROUTE_PROVIDERS,Regionalization_Management_Setting_ROUTE_PROVIDERS],
    };
  }
}
