using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Dignite.Abp.UserPoints;

public interface IMyPointAppService : IApplicationService
{
    /// <summary>
    /// Asynchronously retrieves the number of available points for the current user.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains the number of available points. The
    /// value is zero if no points are available.</returns>
    Task<ListResultDto<UserPointAccountDto>> GetAccountsAsync();

    /// <summary>
    /// Asynchronously retrieves a paged list of user points based on the specified filtering and paging criteria.
    /// </summary>
    /// <param name="input">The input parameters used to filter, sort, and page the list of user points. Cannot be null.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a paged result of user point data
    /// transfer objects that match the specified criteria. The result may be empty if no user points are found.</returns>
    Task<PagedResultDto<UserPointTransactionDto>> GetListAsync(GetUserPointTransactionsInput input);
}
