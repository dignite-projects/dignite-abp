using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Dignite.Abp.UserPoints;

[Area(UserPointsRemoteServiceConsts.ModuleName)]
[RemoteService(Name = UserPointsRemoteServiceConsts.RemoteServiceName)]
[Route("api/UserPoints")]
public class UserPointsItemController : UserPointsController, IUserPointsItemAppService
{
    private readonly IUserPointsItemAppService _userPointsItemAppService;

    public UserPointsItemController(IUserPointsItemAppService userPointsItemAppService)
    {
        _userPointsItemAppService = userPointsItemAppService;
    }

    [HttpGet]
    [Route("get-user-total-points")]
    [Authorize]
    public async Task<int> GetUserTotalPointsAsync(GetUserTotalPointsInput input)
    {
        return await _userPointsItemAppService.GetUserTotalPointsAsync(input);
    }

    [HttpGet]
    [Route("get-user-points-items")]
    public async Task<PagedResultDto<UserPointsItemDto>> GetUserPointsItemsAsync(GetUserPointsItemsInput input)
    {
        return await _userPointsItemAppService.GetUserPointsItemsAsync(input);
    }
}
