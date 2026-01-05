using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Users;

namespace Dignite.Abp.UserPoints;

[Authorize]
public class MyPointAppService : UserPointsAppService, IMyPointAppService
{
    private readonly IUserPointAccountRepository _accountRepository;
    private readonly IUserPointTransactionRepository _transactionRepository;

    public MyPointAppService(IUserPointAccountRepository accountRepository, IUserPointTransactionRepository transactionRepository)
    {
        _accountRepository = accountRepository;
        _transactionRepository = transactionRepository;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public async Task<ListResultDto<UserPointAccountDto>> GetAccountsAsync()
    {
        var allAccounts = await _accountRepository.GetListAsync(
            x => x.UserId == CurrentUser.GetId() 
        );

        var dto = ObjectMapper.Map<List<UserPointAccount>, List<UserPointAccountDto>>(allAccounts);
        return new ListResultDto<UserPointAccountDto>(dto);
    }

    /// <summary>
    /// Retrieves a paged list of user points that match the specified filter criteria.
    /// </summary>
    /// <param name="input">An object containing the filter and paging options to apply when retrieving user points. Must not be null.</param>
    /// <returns>A <see cref="PagedResultDto{UserPointTransactionDto}"/> containing the total count and a list of user points that match the
    /// specified criteria. The list may be empty if no user points are found.</returns>
    public async Task<PagedResultDto<UserPointTransactionDto>> GetListAsync(GetUserPointTransactionsInput input)
    {
        var count = await _transactionRepository.GetCountAsync(
            CurrentUser.Id.Value,
            input.AccountId,
            entityType : input.EntityType,
            entityId : input.EntityId
            );
        var result = await _transactionRepository.GetListAsync(
            CurrentUser.Id.Value,
            input.AccountId,
            entityType: input.EntityType,
            entityId: input.EntityId,
            skipCount : input.SkipCount,
            maxResultCount : input.MaxResultCount
            );

        return new PagedResultDto<UserPointTransactionDto>(
            count, 
            ObjectMapper.Map<List<UserPointTransaction>, List<UserPointTransactionDto>>(result)
            );
    }
}
