using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Dignite.Abp.UserPoints;

[Area(UserPointsRemoteServiceConsts.ModuleName)]
[RemoteService(Name = UserPointsRemoteServiceConsts.RemoteServiceName)]
[Route("api/user-points/my-point")]
public class MyPointController : UserPointsController, IMyPointAppService
{
    private readonly IMyPointAppService _pointsAppService;

    public MyPointController(IMyPointAppService pointsAppService)
    {
        _pointsAppService = pointsAppService;
    }

    [HttpGet]
    [Route("available-point")]
    [Authorize]
    public async Task<int> GetAvailableAsync()
    {
        return await _pointsAppService.GetAvailableAsync();
    }

    [HttpGet]
    [Authorize]
    public async Task<PagedResultDto<UserPointDto>> GetListAsync(GetUserPointListInput input)
    {
        return await _pointsAppService.GetListAsync(input);
    }
}
