using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Dignite.Abp.UserPoints;

[Area(UserPointsRemoteServiceConsts.ModuleName)]
[RemoteService(Name = UserPointsRemoteServiceConsts.RemoteServiceName)]
[Route("api/UserPoints")]
public class UserPointsOrderController : UserPointsController, IUserPointsOrderAppService
{
    private readonly IUserPointsOrderAppService _userPointsOrderAppService;

    public UserPointsOrderController(IUserPointsOrderAppService userPointsOrderAppService)
    {
        _userPointsOrderAppService = userPointsOrderAppService;
    }

    [HttpGet]
    [Route("my-orders")]
    public async Task<PagedResultDto<UserPointsOrderDto>> GetMyOrdersAsync(GetMyOrdersInput input)
    {
        return await _userPointsOrderAppService.GetMyOrdersAsync(input);
    }
}
