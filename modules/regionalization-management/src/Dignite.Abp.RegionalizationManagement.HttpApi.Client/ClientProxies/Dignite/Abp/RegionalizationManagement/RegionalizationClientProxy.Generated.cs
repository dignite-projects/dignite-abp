// This file is automatically generated by ABP framework to use MVC Controllers from CSharp
using Dignite.Abp.RegionalizationManagement;
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
namespace Dignite.Abp.RegionalizationManagement;

[Dependency(ReplaceServices = true)]
[ExposeServices(typeof(IRegionalizationAppService), typeof(RegionalizationClientProxy))]
public partial class RegionalizationClientProxy : ClientProxyBase<IRegionalizationAppService>, IRegionalizationAppService
{
    public virtual async Task<RegionalizationDto> GetAsync()
    {
        return await RequestAsync<RegionalizationDto>(nameof(GetAsync));
    }

    public virtual async Task<RegionalizationDto> UpdateAsync(UpdateRegionalizationInput input)
    {
        return await RequestAsync<RegionalizationDto>(nameof(UpdateAsync), new ClientProxyRequestTypeValue
        {
            { typeof(UpdateRegionalizationInput), input }
        });
    }
}