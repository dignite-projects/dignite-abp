import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

export const environment = {
  production: true,
  application: {
    baseUrl: 'http://localhost:4200/',
    name: 'TenantDomains',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:44301/',
    redirectUri: baseUrl,
    clientId: 'MultiTenancyDomains_App',
    responseType: 'code',
    scope: 'offline_access TenantDomains',
    requireHttps: true,
  },
  apis: {
    default: {
      url: 'https://localhost:44301',
      rootNamespace: 'Dignite.Abp.MultiTenancyDomains',
    },
    TenantDomains: {
      url: 'https://localhost:44300',
      rootNamespace: 'Dignite.Abp.MultiTenancyDomains',
    },
  },
} as Environment;
