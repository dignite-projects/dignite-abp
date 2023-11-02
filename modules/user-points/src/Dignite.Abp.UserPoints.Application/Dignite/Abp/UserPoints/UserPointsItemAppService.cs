using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;

namespace Dignite.Abp.UserPoints;

public class UserPointsItemAppService : UserPointsAppService, IUserPointsItemAppService
{
    private readonly IUserPointsItemRepository _userPointsItemRepository;
    private readonly IUserPointsBlockRepository _userPointsBlockRepository;

    public UserPointsItemAppService(IUserPointsItemRepository userPointsItemRepository, IUserPointsBlockRepository userPointsBlockRepository)
    {
        _userPointsItemRepository = userPointsItemRepository;
        _userPointsBlockRepository = userPointsBlockRepository;
    }

    [Authorize]
    public async Task<int> GetTotalPointsAsync(GetUserTotalPointsInput input)
    {
        return await _userPointsBlockRepository.GetUserAvailablePointsAsync(
            CurrentUser.Id.Value,
            input.ExpirationDate,
            input.PointsDefinitionName,
            input.PointsWorkflowName);
    }

    [Authorize]
    public async Task<PagedResultDto<UserPointsItemDto>> GetListAsync(GetUserPointsItemsInput input)
    {
        var count = await _userPointsItemRepository.GetCountAsync(
            CurrentUser.Id.Value,
            input.PointsDefinitionName,
            input.PointsWorkflowName,
            input.StartTime,
            input.EndTime);
        var result = await _userPointsItemRepository.GetListAsync(
            CurrentUser.Id.Value,
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
