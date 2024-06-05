import { CoreModule } from '@abp/ng.core';
import { GdprConfigModule } from '@volo/abp.ng.gdpr/config';
import { SettingManagementConfigModule } from '@abp/ng.setting-management/config';
import { ThemeSharedModule } from '@abp/ng.theme.shared';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CommercialUiConfigModule } from '@volo/abp.commercial.ng.ui/config';
import { AccountAdminConfigModule } from '@volo/abp.ng.account/admin/config';
import { AccountPublicConfigModule } from '@volo/abp.ng.account/public/config';
import { AuditLoggingConfigModule } from '@volo/abp.ng.audit-logging/config';
import { IdentityConfigModule } from '@volo/abp.ng.identity/config';
import { LanguageManagementConfigModule } from '@volo/abp.ng.language-management/config';
import { registerLocale } from '@volo/abp.ng.language-management/locale';
import { SaasConfigModule } from '@volo/abp.ng.saas/config';
import { TextTemplateManagementConfigModule } from '@volo/abp.ng.text-template-management/config';
import { ThemeLeptonXModule } from '@volosoft/abp.ng.theme.lepton-x';
import { SideMenuLayoutModule } from '@volosoft/abp.ng.theme.lepton-x/layouts';
import { environment } from '../environments/environment';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { APP_ROUTE_PROVIDER } from './route.provider';
import { OpeniddictproConfigModule } from '@volo/abp.ng.openiddictpro/config';
import { FeatureManagementModule } from '@abp/ng.feature-management';
import { AbpOAuthModule } from '@abp/ng.oauth';
// import { DynamicFormModule } from '@dignite-ng/expand.dynamic-form';
// import { CkEditorModule } from '@dignite-ng/expand.ck-editor';
import { dynamic_form_ROUTE_PROVIDER } from './dynamic-form-test/route.provider';
// import { DynamicFormModule, FieldControlGroup } from '@dignite-ng/expand.dynamic-form';
// import { ckEditorFieldControlGroup } from '@dignite-ng/expand.ck-editor';
import { CmsConfigModule } from '@dignite-ng/expand.cms/config';
import { DynamicFormModule, FieldControlGroup } from '@dignite-ng/expand.dynamic-form';
import { fielFieldControlGroup } from '@dignite-ng/expand.file-explorer';
import { ckEditorFieldControlGroup } from '@dignite-ng/expand.ck-editor';


import { FileConfigModule } from '@dignite-ng/expand.file-explorer/config';



@NgModule({
  declarations: [AppComponent],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    CoreModule.forRoot({
      environment,
      registerLocaleFn: registerLocale(),
    }),
    AbpOAuthModule.forRoot(),
    ThemeSharedModule.forRoot(),
    AccountAdminConfigModule.forRoot(),
    AccountPublicConfigModule.forRoot(),
    IdentityConfigModule.forRoot(),
    LanguageManagementConfigModule.forRoot(),
    SaasConfigModule.forRoot(),
    AuditLoggingConfigModule.forRoot(),
    OpeniddictproConfigModule.forRoot(),
    TextTemplateManagementConfigModule.forRoot(),
    SettingManagementConfigModule.forRoot(),
    ThemeLeptonXModule.forRoot(),
    SideMenuLayoutModule.forRoot(),
    CommercialUiConfigModule.forRoot(),
    FeatureManagementModule.forRoot(),
    GdprConfigModule.forRoot({
      privacyPolicyUrl: 'gdpr-cookie-consent/privacy',
      cookiePolicyUrl: 'gdpr-cookie-consent/cookie',
    }),
    CmsConfigModule.forRoot(),
    DynamicFormModule.forRoot({
      cmsFieldControlGroup: [...FieldControlGroup,...fielFieldControlGroup,...ckEditorFieldControlGroup]
    }),
    FileConfigModule.forRoot()
    
  ],
  providers: [APP_ROUTE_PROVIDER,
    dynamic_form_ROUTE_PROVIDER
  ],
  bootstrap: [AppComponent],
})
export class AppModule { }
