
import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

export const environment = {
  production: false,
  application: {
    baseUrl: 'http://localhost:4200/',
    name: 'Cms',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:44322',
    redirectUri: baseUrl,
    clientId: 'Cms_App',
    responseType: 'code',
    scope: 'offline_access Cms',
    requireHttps: true
  },
  apis: {
    default: {
      url: 'https://localhost:44321',
      rootNamespace: 'Dignite.Cms',
    },
    AbpAccountPublic: {
      url: 'https://localhost:44322',
      rootNamespace: 'AbpAccountPublic',
    },
  },
} as Environment;