import { CoreModule, provideAbpCore, withOptions } from '@abp/ng.core';
import { registerLocale } from '@abp/ng.core/locale';
import {
  InternetConnectionStatusComponent,
  ThemeSharedModule,
  provideAbpThemeShared,
} from '@abp/ng.theme.shared';
import { provideFeatureManagementConfig } from '@abp/ng.feature-management';
import { provideAbpOAuth } from '@abp/ng.oauth';
import { IdentityConfigModule, provideIdentityConfig } from '@abp/ng.identity/config';
import { provideSettingManagementConfig } from '@abp/ng.setting-management/config';
import { provideTenantManagementConfig } from '@abp/ng.tenant-management/config';
import { provideAccountConfig } from '@abp/ng.account/config';
import { ThemeLeptonXModule } from '@abp/ng.theme.lepton-x';
import { SideMenuLayoutModule } from '@abp/ng.theme.lepton-x/layouts';
import { AccountLayoutModule } from '@abp/ng.theme.lepton-x/account';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { environment } from '../environments/environment';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { APP_ROUTE_PROVIDER } from './route.provider';
import { CmsConfigModule } from '@dignite-ng/expand.cms/config';
import { FormConfigLoaderService } from './services/form-config-loader.service';

@NgModule({
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    CoreModule,
    ThemeSharedModule,
    ThemeLeptonXModule.forRoot(),
    SideMenuLayoutModule.forRoot(),
    AccountLayoutModule.forRoot(),
    InternetConnectionStatusComponent,
    CmsConfigModule.forRoot(),
    
  ],
  declarations: [AppComponent,],
  providers: [
    APP_ROUTE_PROVIDER,
    provideAbpCore(
      withOptions({
        environment,
        registerLocaleFn: registerLocale(),
      })
    ),
    provideAbpOAuth(),
    provideAbpThemeShared(),
    provideSettingManagementConfig(),
    provideAccountConfig(),
    provideIdentityConfig(),
    provideTenantManagementConfig(),
    provideFeatureManagementConfig(),
    {
      provide: 'MERGED_FORM_CONFIG',
      useFactory:  (configLoader: FormConfigLoaderService) =>  configLoader.getMergedConfig(),
      deps: [FormConfigLoaderService],
    },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}


// import { NgModule } from '@angular/core';
// import { BrowserModule } from '@angular/platform-browser';
// import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
// import { CoreModule } from '@abp/ng.core';
// import { registerLocale } from '@abp/ng.core/locale';
// import { InternetConnectionStatusComponent, ThemeSharedModule } from '@abp/ng.theme.shared';
// import { ThemeLeptonXModule } from '@abp/ng.theme.lepton-x';
// import { SideMenuLayoutModule } from '@abp/ng.theme.lepton-x/layouts';
// import { IdentityConfigModule } from '@abp/ng.identity/config';
// import { AbpOAuthModule } from '@abp/ng.oauth';
// import { SettingManagementConfigModule } from '@abp/ng.setting-management/config';
// import { TenantManagementConfigModule } from '@abp/ng.tenant-management/config';
// import { FeatureManagementModule } from '@abp/ng.feature-management';
// import { AccountConfigModule } from '@abp/ng.account/config';
// import { AccountLayoutModule } from '@abp/ng.theme.lepton-x/account';
// import { environment } from '../environments/environment';
// import { AppRoutingModule } from './app-routing.module';
// import { AppComponent } from './app.component';
// import { APP_ROUTE_PROVIDER } from './route.provider';

// @NgModule({
//   imports: [
//     BrowserModule,
//     BrowserAnimationsModule,
//     AppRoutingModule,
//     CoreModule.forRoot({
//       environment,
//       registerLocaleFn: registerLocale(),
//       sendNullsAsQueryParam: false,
//       skipGetAppConfiguration: false,
//     }),
//     AbpOAuthModule.forRoot(),
//     ThemeSharedModule.forRoot(),
//     AccountConfigModule.forRoot(),
//     IdentityConfigModule.forRoot(),
//     TenantManagementConfigModule.forRoot(),
//     FeatureManagementModule.forRoot(),
//     SettingManagementConfigModule.forRoot(),
//     ThemeLeptonXModule.forRoot(),
//     SideMenuLayoutModule.forRoot(),
//     AccountLayoutModule.forRoot(),
//     InternetConnectionStatusComponent
//   ],
//   providers: [APP_ROUTE_PROVIDER],
//   declarations: [AppComponent],
//   bootstrap: [AppComponent],
// })
// export class AppModule {}
