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
    public async Task<int> CalculatePointsAsync(CalculateExpiryPointsInput input)
    {
        return await _userPointsItemRepository.CalculatePointsAsync(
            CurrentUser.Id.Value,
            input.ExpirationDate,
            input.PointsType,
            input.PointsDefinitionName,
            input.PointsWorkflowName);
    }

    [Authorize]
    public async Task<PagedResultDto<UserPointsItemDto>> GetMyPointsAsync(GetMyPointsInput input)
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
