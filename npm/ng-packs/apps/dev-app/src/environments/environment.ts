//原数据

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
//     issuer: 'https://localhost:44322',
//     redirectUri: baseUrl,
//     clientId: 'Cms_App',
//     responseType: 'code',
//     scope: 'offline_access Cms',
//     requireHttps: true
//   },
//   apis: {
//     default: {
//       url: 'https://localhost:44321',
//       rootNamespace: 'Dignite.Cms',
//     },
//     AbpAccountPublic: {
//       url: 'https://localhost:44322',
//       rootNamespace: 'AbpAccountPublic',
//     },
//   },
// } as Environment;



/* trevely配置 */


import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

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


const oAuthConfig = {
  issuer: 'https://auth.dignite.dev/',
  redirectUri: baseUrl,
  clientId: 'Travely_App',
  responseType: 'code',
  scope: 'offline_access Travely',
  requireHttps: true,
  impersonation: {
    userImpersonation: true,
    tenantImpersonation: true,
  },
  /*
     /**
     * postLogoutRedirectUri?: string;
     * 含义：注销后重定向URI。用户成功注销后，认证服务器会将用户重定向到此URI。
     * 用法：可选字段。设置为用户注销后希望跳转到的页面的URL。此URI也需要在认证服务器上为客户端进行配置。
     */
  // postLogoutRedirectUri: 'https://e6dec8df-936.travely.dignite.com/admin',
    /**
     * redirectUriAsPostLogoutRedirectUriFallback?: boolean;
     * 含义：当 `postLogoutRedirectUri` 未设置时，是否使用 `redirectUri` 作为注销后重定向的备选URI。
     * 用法：如果设置为 `true` 且 `postLogoutRedirectUri` 未提供，则注销后会重定向到 `redirectUri`。默认为 `false`。
     */
  // redirectUriAsPostLogoutRedirectUriFallback:true,
 
};

export const environment = {
  production: false,
  application: {
    baseUrl,
    name: 'Travely',
  },
  oAuthConfig,
  apis: {
    default: {
      url: 'https://travely.services.dignite.dev',
      rootNamespace: 'Dignite.Travely.Admin',
    },
    AbpAccountPublic: {
      url: oAuthConfig.issuer,
      rootNamespace: 'AbpAccountPublic',
    },
    AuthPublic: {
      url: oAuthConfig.issuer,
      rootNamespace: 'AuthPublic',
    },
  },
} as Environment;





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


