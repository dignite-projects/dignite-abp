import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

export const environment = {
  production: true,
  application: {
    baseUrl,
    name: 'DevSample',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:44388/',
    redirectUri: baseUrl,
    clientId: 'DevSample_App',
    responseType: 'code',
    scope: 'offline_access DevSample',
    requireHttps: true
  },
  apis: {
    default: {
      url: 'https://localhost:44388',
      rootNamespace: 'DevSample',
    },
  },
} as Environment;
