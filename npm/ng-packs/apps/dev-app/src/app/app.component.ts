import { ConfigStateService } from '@abp/ng.core';
import { Component } from '@angular/core';
import { OAuthService } from 'angular-oauth2-oidc';

@Component({
  selector: 'app-root',
  template: `
    <abp-loader-bar></abp-loader-bar>
    <abp-dynamic-layout></abp-dynamic-layout>
    <abp-gdpr-cookie-consent></abp-gdpr-cookie-consent>
  `,
}) 
export class AppComponent {
  constructor(private oa: OAuthService, private config: ConfigStateService) {
    const tenantId = this.config.getDeep('currentTenant.id');
    if(!tenantId) return
    this.oa.customQueryParams = {
      __tenant: tenantId,
    };
  }
  
}
