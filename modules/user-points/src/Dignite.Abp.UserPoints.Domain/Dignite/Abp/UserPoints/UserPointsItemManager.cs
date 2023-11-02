using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dignite.Abp.Points;
using Microsoft.Extensions.Options;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Dignite.Abp.UserPoints;
public class UserPointsItemManager: DomainService
{
    public UserPointsItemManager(IOptions<DignitePointsBlockOptions> options, IUserPointsItemRepository userPointsItemRepository, IUserPointsBlockRepository userPointsBlockRepository)
    {
        PointsBlockOptions = options.Value;
        UserPointsItemRepository = userPointsItemRepository;
        UserPointsBlockRepository = userPointsBlockRepository;
    }

    protected DignitePointsBlockOptions PointsBlockOptions { get; }

    protected IUserPointsItemRepository UserPointsItemRepository { get; }

    protected IUserPointsBlockRepository UserPointsBlockRepository { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="pointsType"></param>
    /// <param name="pointsDefinitionName"></param>
    /// <param name="pointsWorkflowName"></param>
    /// <param name="points">
    /// The points value is usually calculated by the <see cref="IPointsManager.CalculatePointsAsync(string, string, RulesEngine.Models.ReSettings, object)" /> or <see cref="IPointsManager.CalculatePointsAsync(string, string, RulesEngine.Models.ReSettings, RulesEngine.Models.RuleParameter[])" /> methods
    /// </param>
    /// <param name="expirationDate"></param>
    /// <param name="userId"></param>
    /// <param name="tenantId"></param>
    /// <returns></returns>
    /// <exception cref="AbpException"></exception>
    /// <exception cref="PointsNonFactorValueException"></exception>
    public virtual async Task<UserPointsItem> CreateAsync(
        PointsType pointsType,
        string pointsDefinitionName,
        string pointsWorkflowName,
        int points,
        DateTime expirationDate,
        Guid userId,
        Guid? tenantId)
    {

        if (points < 1)
        {
            throw new AbpException("The points value must be greater than or equal to 1");
        }
        if (points % PointsBlockOptions.Factor != 0)
        {
            throw new PointsNonFactorValueException(points,PointsBlockOptions.Factor);
        }

        var pointsItem = new UserPointsItem(
            GuidGenerator.Create(),
            pointsDefinitionName,
            pointsWorkflowName,
            pointsType,
            points,
            expirationDate,
            userId,
            tenantId);

        // Creating Points Item
        await UserPointsItemRepository.InsertAsync(pointsItem);

        // Creating points blocks
        var pointsBlocks =new List<UserPointsBlock>();
        for (int i=0;i< pointsItem.Points/PointsBlockOptions.Factor;i++)
        {
            pointsBlocks.Add(new UserPointsBlock(pointsItem.Id));
        }
        await UserPointsBlockRepository.InsertManyAsync(pointsBlocks);

        // return UserPointsItem
        return pointsItem;
    }

    public virtual async Task DeleteAsync(UserPointsItem pointsItem)
    {
        if (pointsItem.PointsBlocks.Any(b => b.IsLocked))
        {
            throw new RelatedLockedPointsException(pointsItem.Points);
        }

        await UserPointsItemRepository.DeleteAsync(pointsItem);
    }
}
