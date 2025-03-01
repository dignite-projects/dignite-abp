import { ModuleWithProviders, NgModule } from '@angular/core';
import { PageModule } from '@abp/ng.components/page';
import { CoreModule } from '@abp/ng.core';
import { ThemeSharedModule } from '@abp/ng.theme.shared';
import { CommonModule } from '@angular/common';
import { CMS_KIT_ROUTE_PROVIDERS } from './providers/route.provider';
import { GetTenantImgPipe, ImagePreviewComponent } from '@dignite-ng/expand.core';

@NgModule({
  imports: [
    CoreModule,
    ThemeSharedModule,
    CommonModule,
    PageModule,
    ImagePreviewComponent,
    GetTenantImgPipe,
  ],
  providers: [GetTenantImgPipe],
  declarations: [
    // BrandComponent, 
    // LogoComponent
  ],
  exports: [],
})
export class CmsKitConfigModule {
  static forRoot(): ModuleWithProviders<CmsKitConfigModule> {
    return {
      ngModule: CmsKitConfigModule,
      providers: [CMS_KIT_ROUTE_PROVIDERS],
    };
  }
}
