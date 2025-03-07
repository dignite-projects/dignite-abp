import { NgModule, NgModuleFactory, ModuleWithProviders } from '@angular/core';
import { CoreModule, LazyModuleFactory } from '@abp/ng.core';
import { ThemeSharedModule } from '@abp/ng.theme.shared';
import { TenantDomainsComponent } from './components/tenant-domain-management.component';
import { TenantDomainsRoutingModule } from './tenant-domain-management-routing.module';

@NgModule({
  declarations: [TenantDomainsComponent],
  imports: [CoreModule, ThemeSharedModule, TenantDomainsRoutingModule],
  exports: [TenantDomainsComponent],
})
export class TenantDomainsModule {
  static forChild(): ModuleWithProviders<TenantDomainsModule> {
    return {
      ngModule: TenantDomainsModule,
      providers: [],
    };
  }

  static forLazy(): NgModuleFactory<TenantDomainsModule> {
    return new LazyModuleFactory(TenantDomainsModule.forChild());
  }
}
