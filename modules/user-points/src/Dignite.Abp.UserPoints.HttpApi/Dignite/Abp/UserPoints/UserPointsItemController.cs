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
    [Route("calculate-points")]
    [Authorize]
    public async Task<int> CalculatePointsAsync(CalculateExpiryPointsInput input)
    {
        return await _userPointsItemAppService.CalculatePointsAsync(input);
    }

    [HttpGet]
    [Route("my-points")]
    public async Task<PagedResultDto<UserPointsItemDto>> GetMyPointsAsync(GetMyPointsInput input)
    {
        return await _userPointsItemAppService.GetMyPointsAsync(input);
    }
}
