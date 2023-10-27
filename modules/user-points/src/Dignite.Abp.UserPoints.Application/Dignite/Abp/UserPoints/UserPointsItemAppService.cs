using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;

namespace Dignite.Abp.UserPoints;

public class UserPointsItemAppService : UserPointsAppService, IUserPointsItemAppService
{
    private readonly IUserPointsItemRepository _userPointsItemRepository;

    public UserPointsItemAppService(IUserPointsItemRepository userPointsItemRepository)
    {
        _userPointsItemRepository = userPointsItemRepository;
    }

    [Authorize]
    public async Task<int> GetUserTotalPointsAsync(GetUserTotalPointsInput input)
    {
        return await _userPointsItemRepository.GetUserTotalPointsAsync(
            CurrentUser.Id.Value,
            input.ExpirationDate,
            input.PointsType,
            input.PointsDefinitionName,
            input.PointsWorkflowName);
    }

    [Authorize]
    public async Task<PagedResultDto<UserPointsItemDto>> GetUserPointsItemsAsync(GetUserPointsItemsInput input)
    {
        var count = await _userPointsItemRepository.GetCountAsync(
            CurrentUser.Id.Value,
            input.PointsType,
            input.PointsDefinitionName,
            input.PointsWorkflowName,
            input.StartTime,
            input.EndTime);
        var result = await _userPointsItemRepository.GetListAsync(
            CurrentUser.Id.Value,
            input.PointsType,
            input.PointsDefinitionName,
            input.PointsWorkflowName,
            input.StartTime,
            input.EndTime,
            input.MaxResultCount,
            input.SkipCount);

        return new PagedResultDto<UserPointsItemDto>(
            count, 
            ObjectMapper.Map<List<UserPointsItem>, List<UserPointsItemDto>>(result)
            );
    }
}
