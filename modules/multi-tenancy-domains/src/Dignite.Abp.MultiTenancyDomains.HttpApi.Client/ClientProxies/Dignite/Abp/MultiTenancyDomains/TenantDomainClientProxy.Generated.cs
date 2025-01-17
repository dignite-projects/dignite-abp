// This file is automatically generated by ABP framework to use MVC Controllers from CSharp
using Dignite.Abp.MultiTenancyDomains;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Http.Client.ClientProxying;
using Volo.Abp.Http.Modeling;

// ReSharper disable once CheckNamespace
namespace Dignite.Abp.MultiTenancyDomains;

[Dependency(ReplaceServices = true)]
[ExposeServices(typeof(ITenantDomainAppService), typeof(TenantDomainClientProxy))]
public partial class TenantDomainClientProxy : ClientProxyBase<ITenantDomainAppService>, ITenantDomainAppService
{
    public virtual async Task<bool> DomainNameExistsAsync(string domainName)
    {
        return await RequestAsync<bool>(nameof(DomainNameExistsAsync), new ClientProxyRequestTypeValue
        {
            { typeof(string), domainName }
        });
    }

    public virtual async Task<TenantDomainDto> FindByCurrentTenantAsync()
    {
        return await RequestAsync<TenantDomainDto>(nameof(FindByCurrentTenantAsync));
    }

    public virtual async Task<TenantDomainDto> FindByDomainNameAsync(string domainName)
    {
        return await RequestAsync<TenantDomainDto>(nameof(FindByDomainNameAsync), new ClientProxyRequestTypeValue
        {
            { typeof(string), domainName }
        });
    }

    public virtual async Task<TenantDomainDto> UpdateAsync(UpdateTenantDomainInput input)
    {
        return await RequestAsync<TenantDomainDto>(nameof(UpdateAsync), new ClientProxyRequestTypeValue
        {
            { typeof(UpdateTenantDomainInput), input }
        });
    }
}
