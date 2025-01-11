import { ModuleWithProviders, NgModule } from '@angular/core';
import { TENANT_DOMAINS_ROUTE_PROVIDERS } from './providers/route.provider';

@NgModule()
export class TenantDomainsConfigModule {
  static forRoot(): ModuleWithProviders<TenantDomainsConfigModule> {
    return {
      ngModule: TenantDomainsConfigModule,
      providers: [TENANT_DOMAINS_ROUTE_PROVIDERS],
    };
  }
}
