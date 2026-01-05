using System.Threading.Tasks;
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
    [Route("accounts")]
    public async Task<ListResultDto<UserPointAccountDto>> GetAccountsAsync()
    {
        return await _pointsAppService.GetAccountsAsync();
    }

    [HttpGet]
    [Route("transactions")]
    public async Task<PagedResultDto<UserPointTransactionDto>> GetListAsync(GetUserPointTransactionsInput input)
    {
        return await _pointsAppService.GetListAsync(input);
    }
}
