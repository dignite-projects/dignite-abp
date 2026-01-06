using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp.Domain.Services;

namespace Dignite.Abp.UserPoints;
public class UserPointAccountManager: DomainService
{    
    protected IUserPointTypeDefinitionStore PointTypeDefinitionStore { get; }

    protected IUserPointEntityTypeDefinitionStore EntityTypeDefinitionStore { get; }
    protected IUserPointAccountRepository AccountRepository { get; }

    protected IUserPointTransactionRepository TransactionRepository { get; }

    public UserPointAccountManager(IUserPointTypeDefinitionStore pointTypeDefinitionStore, IUserPointEntityTypeDefinitionStore entityTypeDefinitionStore,
        IUserPointAccountRepository pointAccountRepository, IUserPointTransactionRepository pointTransactionRepository)
    {
        PointTypeDefinitionStore = pointTypeDefinitionStore;
        EntityTypeDefinitionStore = entityTypeDefinitionStore;
        AccountRepository = pointAccountRepository;
        TransactionRepository = pointTransactionRepository;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="amount"></param>
    /// <param name="pointTypeName"></param>
    /// <param name="expirationTime"></param>
    /// <param name="entityType"></param>
    /// <param name="entityId"></param>
    /// <param name="remark"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <exception cref="UnsupportedPointTypeException"></exception>
    /// <exception cref="EntityNotPointableException"></exception>
    public virtual async Task<UserPointAccount> EarnAsync(
        Guid userId, 
        [ValueRange(1,int.MaxValue)] int amount,
        [NotNull] string pointTypeName, 
        DateTime? expirationTime = null,
        [CanBeNull] string entityType = null, [CanBeNull] string entityId = null,
        [CanBeNull] string remark = null
        )
    {
        if (amount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "When adding points, the amount must be a positive number.");
        }

        if (!await PointTypeDefinitionStore.IsDefinedAsync(pointTypeName))
        {
            throw new UnsupportedPointTypeException(pointTypeName);
        }

        if (!entityType.IsNullOrEmpty() && !await EntityTypeDefinitionStore.IsDefinedAsync(entityType))
        {
            throw new EntityNotPointableException(entityType);
        }

        var pointTypeDefinition = await PointTypeDefinitionStore.GetAsync(pointTypeName);

        if (!pointTypeDefinition.IsEnabled)
        {
            throw new UserPointTypeNotEnabledException(pointTypeName);
        }

        //
        var account = await AccountRepository.FindAsync(
            x => x.UserId == userId && x.PointTypeName == pointTypeName,
            false
        );
        if (account == null)
        {
            account = new UserPointAccount
            (
                GuidGenerator.Create(),
                userId,
                pointTypeName,
                amount,
                Clock.Now,
                CurrentTenant.Id
            );
            await AccountRepository.InsertAsync(account);
        }
        else
        {
            account.AddTotalEarned(amount, Clock.Now);
            await AccountRepository.UpdateAsync(account);
        }

        // Creating User Point Object
        var transaction = new UserPointTransaction(
            GuidGenerator.Create(),
            userId, amount, account.CurrentBalance,
            UserPointTransactionType.Earn, account.Id, pointTypeDefinition.ConsumptionPriority,
            expirationTime.HasValue ? expirationTime.Value : Clock.Now.AddDays(pointTypeDefinition.DefaultExpirationDays),
            entityType, entityId,
            remark,
            CurrentTenant.Id);

        await TransactionRepository.InsertAsync(transaction);

        // returning
        return account;
    }

    public virtual async Task<IList<UserPointAccount>> ConsumeAsync(
        Guid userId,
        [ValueRange(int.MinValue,-1)]int amount,  
        [NotNull] string[] pointTypeNames=null,     
        [CanBeNull] string entityType = null, [CanBeNull] string entityId = null,
        [CanBeNull] string remark = null
        )
    {
        if (amount > 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Amount must be a negative number when consuming points.");
        }

        if (pointTypeNames != null && pointTypeNames.Any())
        {
            foreach (var pointTypeName in pointTypeNames)
            {
                if (!await PointTypeDefinitionStore.IsDefinedAsync(pointTypeName))
                {
                    throw new UnsupportedPointTypeException(pointTypeName);
                }
            }
        }

        if (!entityType.IsNullOrEmpty() && !await EntityTypeDefinitionStore.IsDefinedAsync(entityType))
        {
            throw new EntityNotPointableException(entityType);
        }


        var allAccounts = await AccountRepository.GetListAsync(
            x => x.UserId == userId && x.Status == UserPointAccountStatus.Active
        );

        var accounts = allAccounts.WhereIf(pointTypeNames != null && pointTypeNames.Any(), x => pointTypeNames.Contains(x.PointTypeName))
        .ToList();


        var currentBalance = accounts.Sum(x => x.CurrentBalance);
        if (currentBalance < -amount) //负负得正
        {
            throw new InsufficientPointException(currentBalance, -amount); //余额不足 
        }

        var transactions = await TransactionRepository.ConsumeAsync(userId, amount, accounts.Select(a=>a.Id), entityType, entityId, remark);
        foreach (var transaction in transactions)
        {
            var account = accounts.Find(x => x.Id == transaction.AccountId);

            // 更新账户
            account.AddTotalSpent(transaction.Amount, Clock.Now);
            await AccountRepository.UpdateAsync(account);

            // 添加交易记录
            transaction.SetBalance(account.CurrentBalance);
            await TransactionRepository.InsertAsync(transaction);
        }

        return allAccounts;
    }
}
