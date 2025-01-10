import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

export const environment = {
  production: true,
  application: {
    baseUrl: 'http://localhost:4200/',
    name: 'RegionalizationManagement',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:44301/',
    redirectUri: baseUrl,
    clientId: 'RegionalizationManagement_App',
    responseType: 'code',
    scope: 'offline_access RegionalizationManagement',
    requireHttps: true,
  },
  apis: {
    default: {
      url: 'https://localhost:44301',
      rootNamespace: 'Dignite.Abp.RegionalizationManagement',
    },
    RegionalizationManagement: {
      url: 'https://localhost:44300',
      rootNamespace: 'Dignite.Abp.RegionalizationManagement',
    },
  },
} as Environment;
