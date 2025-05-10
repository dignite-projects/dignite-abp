//原数据

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



/* trevely配置 */


// import { Environment } from '@abp/ng.core';

// const baseUrl = 'http://localhost:4200';

// const oAuthConfig = {
//   issuer: 'https://localhost:44361/',
//   redirectUri: baseUrl,
//   clientId: 'Travely_App',
//   responseType: 'code',
//   scope: 'offline_access Travely',
//   requireHttps: true,
// };

// export const environment = {
//   production: false,
//   application: {
//     baseUrl,
//     name: 'Travely',
//   },
//   oAuthConfig,
//   apis: {
//     default: {
//       url: 'https://localhost:44390',
//       rootNamespace: 'Dignite.Travely.Admin',
//     },
//     AbpAccountPublic: {
//       url: oAuthConfig.issuer,
//       rootNamespace: 'AbpAccountPublic',
//     },
//     AuthPublic: {
//       url: oAuthConfig.issuer,
//       rootNamespace: 'AuthPublic',
//     },
//   },
// } as Environment;
 
/* website */
// import { Environment } from '@abp/ng.core';

// const baseUrl = 'http://localhost:4200';

// export const environment = {
//   production: false,
//   application: {
//     baseUrl: 'http://localhost:4200/',
//     name: 'Website',
//     logoUrl: '',
//   },
//   oAuthConfig: {
//     issuer: 'https://localhost:44361/',
//     redirectUri: baseUrl,
//     clientId: 'Website_App',
//     responseType: 'code',
//     scope: 'offline_access Website',
//     requireHttps: true,
//   },
//   apis: {
//     default: {
//       url: 'https://localhost:44356',
//       rootNamespace: 'Dignite.Website',
//     },
//     AbpAccountPublic: {
//       url: 'https://localhost:44361',
//       rootNamespace: 'AbpAccountPublic',
//     },
//     OpenIddictPro:{
//       // url: oAuthConfig.issuer,
//       url: 'https://localhost:44361/',
//       rootNamespace: 'OpenIddictPro',
//     },
//     SaasHost: {
//       url: 'https://localhost:44361/',
//       rootNamespace: 'SaasHost',
//     },
//     SaasTenant: {
//       url: 'https://localhost:44361/',
//       rootNamespace: 'SaasTenant',
//     },
//   },
// } as Environment;

/** */
