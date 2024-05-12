import { ModuleWithProviders, NgModule } from '@angular/core';
import { CMS_ROUTE_PROVIDERS } from './providers/route.provider';

@NgModule()
export class CmsConfigModule {
  static forRoot(): ModuleWithProviders<CmsConfigModule> {
    return {
      ngModule: CmsConfigModule,
      providers: [CMS_ROUTE_PROVIDERS],
    };
  }
}
