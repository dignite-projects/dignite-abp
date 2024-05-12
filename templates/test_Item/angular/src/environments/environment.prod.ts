import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

const oAuthConfig = {
  issuer: 'https://localhost:44329/',
  redirectUri: baseUrl,
  clientId: 'test_Item_App',
  responseType: 'code',
  scope: 'offline_access test_Item',
  requireHttps: true,
};

export const environment = {
  production: true,
  application: {
    baseUrl,
    name: 'test_Item',
  },
  oAuthConfig,
  apis: {
    default: {
      url: 'https://localhost:44329',
      rootNamespace: 'test_Item',
    },
    AbpAccountPublic: {
      url: oAuthConfig.issuer,
      rootNamespace: 'AbpAccountPublic',
    },
  },
  remoteEnv: {
    url: '/getEnvConfig',
    mergeStrategy: 'deepmerge'
  }
} as Environment;
