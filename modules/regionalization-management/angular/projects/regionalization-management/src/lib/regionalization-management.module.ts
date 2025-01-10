import { NgModule, NgModuleFactory, ModuleWithProviders } from '@angular/core';
import { CoreModule, LazyModuleFactory } from '@abp/ng.core';
import { ThemeSharedModule } from '@abp/ng.theme.shared';
import { RegionalizationManagementComponent } from './components/regionalization-management.component';
import { RegionalizationManagementRoutingModule } from './regionalization-management-routing.module';

@NgModule({
  declarations: [RegionalizationManagementComponent],
  imports: [CoreModule, ThemeSharedModule, RegionalizationManagementRoutingModule],
  exports: [RegionalizationManagementComponent],
})
export class RegionalizationManagementModule {
  static forChild(): ModuleWithProviders<RegionalizationManagementModule> {
    return {
      ngModule: RegionalizationManagementModule,
      providers: [],
    };
  }

  static forLazy(): NgModuleFactory<RegionalizationManagementModule> {
    return new LazyModuleFactory(RegionalizationManagementModule.forChild());
  }
}
