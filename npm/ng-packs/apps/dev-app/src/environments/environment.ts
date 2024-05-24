/**测试相关 */
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
  production: false,
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
} as Environment;



/**cms相关 */
// import { Environment } from '@abp/ng.core';

// const baseUrl = 'http://localhost:4200';

// export const environment = {
//   production: false,
//   application: {
//     baseUrl: 'http://localhost:4200/',
//     name: 'Cms',
//     logoUrl: '',
//   },
//   oAuthConfig: {
//     issuer: 'https://localhost:44361/',
//     redirectUri: baseUrl,
//     clientId: 'Auth_App',
//     responseType: 'code',
//     scope: 'offline_access Auth',
//     requireHttps: true
//   },
//   apis: {
//     default: {
//       url: 'https://localhost:44356',
//       rootNamespace: 'Dignite.Cms',
//     },
//     AbpAccountPublic: {
//       url: 'https://localhost:44361',
//       rootNamespace: 'AbpAccountPublic',
//     },
//   },
// } as Environment;
