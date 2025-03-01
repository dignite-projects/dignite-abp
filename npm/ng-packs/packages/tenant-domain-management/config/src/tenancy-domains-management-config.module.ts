import { ModuleWithProviders, NgModule } from '@angular/core';
import { Tenancy_Domains_Management_ROUTE_PROVIDERS } from './providers/route.provider';
import { Tenancy_Domains_Management_Setting_ROUTE_PROVIDERS } from './providers';

@NgModule()
export class TenancyDomainsManagementConfigModule {
  static forRoot(): ModuleWithProviders<TenancyDomainsManagementConfigModule> {
    return {
      ngModule: TenancyDomainsManagementConfigModule,
      providers: [Tenancy_Domains_Management_Setting_ROUTE_PROVIDERS,Tenancy_Domains_Management_ROUTE_PROVIDERS],
    };
  }
}
