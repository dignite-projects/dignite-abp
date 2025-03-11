import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CoreModule, provideAbpCore, SubscriptionService, withOptions } from '@abp/ng.core';
import { registerLocale } from '@abp/ng.core/locale';
import {
	InternetConnectionStatusComponent,
	ThemeSharedModule,
	provideAbpThemeShared,
} from '@abp/ng.theme.shared';
import { ThemeLeptonXModule } from '@abp/ng.theme.lepton-x';
import { SideMenuLayoutModule } from '@abp/ng.theme.lepton-x/layouts';
import { provideAbpOAuth } from '@abp/ng.oauth';
import { provideSettingManagementConfig } from '@abp/ng.setting-management/config';
import { provideAccountConfig } from '@abp/ng.account/config';
import { provideIdentityConfig } from '@abp/ng.identity/config';
import { provideTenantManagementConfig } from '@abp/ng.tenant-management/config';
import { provideFeatureManagementConfig } from '@abp/ng.feature-management';
import { AccountLayoutModule } from '@abp/ng.theme.lepton-x/account';
import { environment } from '../environments/environment';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { APP_ROUTE_PROVIDER } from './route.provider';
import { OAuthService } from 'angular-oauth2-oidc';
import { CmsConfigModule } from '@dignite-ng/expand.cms/config';
import { FormConfigLoaderService } from './services/form-config-loader.service';
import { Select_ROUTE_PROVIDER } from './select/route.provider';

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
	providers: [
		APP_ROUTE_PROVIDER,
		Select_ROUTE_PROVIDER,
		SubscriptionService,
		OAuthService,
		provideAbpCore(
			withOptions({
				environment,
				registerLocaleFn: registerLocale(),
				sendNullsAsQueryParam: false,
				skipGetAppConfiguration: false,
			}),
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
			useFactory: (configLoader: FormConfigLoaderService) => configLoader.getMergedConfig(),
			deps: [FormConfigLoaderService],
		},
	],
	declarations: [AppComponent],
	bootstrap: [AppComponent],
})
export class AppModule {}
