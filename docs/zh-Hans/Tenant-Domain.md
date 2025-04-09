# 租户域名

## IWebServerManager

`IWebServerManager` 接口提供了一系列方法来管理 Web Server 的操作，包括：

- 添加或更新域名：

  ```csharp
  Task AddOrUpdateDomainAsync(string domain, string upstreamAddress, Guid tenantId, string site = null);
  ```
  - domain: 添加或更新的域名
  - upstreamAddress: 反向代理的地址
  - tenantId: 域名对应的租户Id
  - site: WebServer 的站点名称
  
- 移除域名

  ```csharp
  Task RemoveDomainAsync(string domain, string site = null);
  ```
  - domain: 移除的域名
  - site: WebServer 的站点名称

- 检测域名SSL证书
  ```csharp
  Task<bool> CheckCertificateValidityAsync(string domain);
  ```  
  - domain: 检测的域名

### NullWebServerManager

NullWebServerManager 是一个内置类，它实现了 IWebServerManager，但会将WebServer管理信息写入标准日志系统，而不是真正的操作WebServer。

该类非常有用，尤其是在开发过程中，一般情况下使用IISExpress。请在应用程序启动模板 DEBUG 模式下使用该类，域层配置如下：

```csharp
#if DEBUG
        context.Services.Replace(ServiceDescriptor.Singleton<IWebServerManager, NullWebServerManager>());
#endif
```  

### Caddy Web Server 的实现

用于管理 Caddy Web Server 的站点域名

### 安装

1. 在您的 Application 项目中安装 `Dignite.Abp.TenantDomain.Caddy` NuGet 包。
2. 在您的模块类的 `[DependsOn(...)]` 属性列表中添加 `AbpTenantDomainCaddyModule`。

### 配置

该软件包将 AbpTenantDomainCaddyOptions 定义为一个简单的选项类:

- ApiEndpoint: Caddy Web Server API的终端地址,默认值为: http://localhost:2019
- HttpClientName: 请求Caddy Web Server API时使用的 HttpClientName, 默认值为 Microsoft.Extensions.Options.Options 中定义的 DefaultName 值

## IAuthServerRedirectUriManager

`IAuthServerRedirectUriManager` 接口提供了一系列方法来管理 Auth Server 的操作，包括：

- 添加允许跳转的应用客户端域名

  ```csharp
  Task AddRedirectDomainAsync(string clientId, string domainName);
  ```
  - clientId: Auth Server定义的应用客户端Id
  - domainName: 授权允许跳转的域名
  
- 移除域名

  ```csharp
  Task RemoveRedirectDomainAsync(string clientId, string domainName);
  ```
  - clientId: Auth Server定义的应用客户端Id
  - domainName: 域名

### OpenIddict 的实现

用于管理 OpenIddict 的授权应用客户端域名

### 安装

1. 在您的 Application 项目中安装 `Dignite.Abp.TenantDomain.OpenIddict` NuGet 包。
2. 在您的模块类的 `[DependsOn(...)]` 属性列表中添加 `AbpTenantDomainOpenIddictModule`。

## 租户域名管理

### 安装

- 将 `Dignite.Abp.TenantDomainManagement.Application.Contracts` Nuget 包安装到 Application.Contracts 项目中

    添加 `AbpTenantDomainManagementApplicationContractsModule` 到 [模块类](https://docs.abp.io/en/abp/latest/Module-Development-Basics) `[DependsOn(...)]`属性列表中。

- 将 `Dignite.Abp.TenantDomainManagement.Application` Nuget 包安装到 Application 项目中

    添加 `FileExplorerApplicatAbpTenantDomainManagementApplicationModuleionModule` 到 [模块类](https://docs.abp.io/en/abp/latest/Module-Development-Basics) `[DependsOn(...)]`属性列表中。

- 将 `Dignite.Abp.TenantDomainManagement.HttpApi` Nuget 包安装到 HttpApi 项目中

    添加 `AbpTenantDomainManagementHttpApiModule` 到 [模块类](https://docs.abp.io/en/abp/latest/Module-Development-Basics) `[DependsOn(...)]`属性列表中。

### 配置

该软件包将 `AbpTenantDomainManagementOptions` 定义为一个简单的选项类:

- TenantDomainFormat: 租户二级域名的格式
- WebServerSiteName: Web Server 中的站点名称, 默认值为 "default"
- ProxyAddress：租房域名反向代理的地址，默认值为 "https://localhost:5000"
- AuthServerClientId：Auth Server 中应用客户端Id

````csharp
Configure<AbpTenantDomainManagementOptions>(options =>
{
    options.TenantDomainFormat = "{0}.travely.dignite.com";
    options.AuthServerClientId = "TenantDomainManagement_App";
});
````

### HttpApi层

### TenantDomainController

- `连接域名`
  接口地址：api/tenant-domain-management/tenant-domain/connect
  Http方法：post
  
- `获取当前租户的域名`
  接口地址：api/tenant-domain-management/tenant-domain
  Http方法：get

- `检测域名的cname解析`
  接口地址：api/tenant-domain-management/tenant-domain/check-cname-record
  Http方法：get
  