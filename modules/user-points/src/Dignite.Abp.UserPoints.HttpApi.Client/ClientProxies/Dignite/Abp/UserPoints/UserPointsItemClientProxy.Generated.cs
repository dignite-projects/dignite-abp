// This file is automatically generated by ABP framework to use MVC Controllers from CSharp
using Dignite.Abp.UserPoints;
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
namespace Dignite.Abp.UserPoints;

[Dependency(ReplaceServices = true)]
[ExposeServices(typeof(IUserPointsItemAppService), typeof(UserPointsItemClientProxy))]
public partial class UserPointsItemClientProxy : ClientProxyBase<IUserPointsItemAppService>, IUserPointsItemAppService
{
    public virtual async Task<int> GetTotalPointsAsync(GetUserTotalPointsInput input)
    {
        return await RequestAsync<int>(nameof(GetTotalPointsAsync), new ClientProxyRequestTypeValue
        {
            { typeof(GetUserTotalPointsInput), input }
        });
    }

    public virtual async Task<PagedResultDto<UserPointsItemDto>> GetListAsync(GetUserPointsItemsInput input)
    {
        return await RequestAsync<PagedResultDto<UserPointsItemDto>>(nameof(GetListAsync), new ClientProxyRequestTypeValue
        {
            { typeof(GetUserPointsItemsInput), input }
        });
    }
}
