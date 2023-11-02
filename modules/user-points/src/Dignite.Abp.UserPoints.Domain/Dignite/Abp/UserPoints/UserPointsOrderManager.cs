using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Volo.Abp.Domain.Services;

namespace Dignite.Abp.UserPoints;
public class UserPointsOrderManager : DomainService
{
    public UserPointsOrderManager(
        IOptions<DignitePointsBlockOptions> options, 
        IUserPointsOrderRepository pointsOrderRepository, 
        IUserPointsBlockRepository pointsBlockRepository,
        UserPointsItemManager pointsItemManager)
    {
        PointsBlockOptions = options.Value;
        PointsOrderRepository = pointsOrderRepository;
        PointsBlockRepository = pointsBlockRepository;
        PointsItemManager = pointsItemManager;
    }

    protected DignitePointsBlockOptions PointsBlockOptions { get; }
    protected IUserPointsOrderRepository PointsOrderRepository { get; }

    protected IUserPointsBlockRepository PointsBlockRepository { get; }
    protected UserPointsItemManager PointsItemManager { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="points"></param>
    /// <param name="businessOrderType"></param>
    /// <param name="businessOrderNumber"></param>
    /// <param name="userId"></param>
    /// <param name="pointsType">Specify the type of points</param>
    /// <param name="pointsDefinitionName">Specify which points to use</param>
    /// <param name="pointsWorkflowName">Specify which processes are used to obtain the points</param>
    /// <param name="tenantId"></param>
    /// <returns></returns>
    /// <exception cref="InsufficientAvailablePointsException"></exception>
    public virtual async Task<UserPointsOrder> CreateAsync(
        int points,
        string businessOrderType,
        string businessOrderNumber, 
        Guid userId,
        PointsType pointsType = PointsType.General, 
        string pointsDefinitionName = null, 
        string pointsWorkflowName = null,
        Guid? tenantId=null)
    {
        var availablePointsBlocks = await PointsBlockRepository.GetTopAvailableListAsync(points / PointsBlockOptions.Factor, userId, pointsType, pointsDefinitionName, pointsWorkflowName);        
        if (availablePointsBlocks.Count < points / PointsBlockOptions.Factor)
        {
            throw new InsufficientAvailablePointsException(points);
        }

        var pointsOrder = new UserPointsOrder(
                GuidGenerator.Create(),
                points,
                businessOrderType,
                businessOrderNumber,
                userId,
                tenantId);

        // Group points by expiration date and add the grouped points to the order
        var pointsBlocksGroup = availablePointsBlocks.GroupBy(
            b => new { 
                b.UserPointsItem.PointsDefinitionName, 
                b.UserPointsItem.PointsWorkflowName,
                b.UserPointsItem.PointsType,
                b.UserPointsItem.ExpirationDate,
            });
        foreach ( var group in pointsBlocksGroup ) 
        {
            pointsOrder.Redeems.Add(
                new UserPointsOrderRedeem(
                    group.Key.PointsDefinitionName, group.Key.PointsWorkflowName,
                    group.Key.PointsType,
                    group.Count() * PointsBlockOptions.Factor, 
                    group.Key.ExpirationDate
                    ));
        }

        // Creating Points order
        await PointsOrderRepository.InsertAsync(pointsOrder);

        // Update block status to locked
        availablePointsBlocks.ForEach(b => b.SetLocked());
        await PointsBlockRepository.UpdateManyAsync(availablePointsBlocks);

        // return UserPointsOrder
        return pointsOrder;
    }

    /// <summary>
    /// Delete a Points Order
    /// </summary>
    /// <param name="pointsOrder"></param>
    /// <param name="shouldRollbackPoints">Whether or not you need to roll back points</param>
    /// <returns></returns>
    public virtual async Task DeleteAsync(UserPointsOrder pointsOrder, bool shouldRollbackPoints)
    {
        if (shouldRollbackPoints)
        {
            foreach (var redeem in pointsOrder.Redeems)
            {
                await PointsItemManager.CreateAsync(
                    redeem.PointsDefinitionName,
                    redeem.PointsWorkflowName,
                    redeem.PointsType,
                    redeem.Points,
                    redeem.ExpirationDate,
                    pointsOrder.UserId,
                    pointsOrder.TenantId
                    );
            }
        }

        await PointsOrderRepository.DeleteAsync(pointsOrder);
    }

    public virtual async Task<UserPointsOrder> FindByBusinessOrderAsync(string businessOrderType, string businessOrderNumber)
    {
        return await PointsOrderRepository.FindByBusinessOrderAsync(businessOrderType, businessOrderNumber);
    }
}
