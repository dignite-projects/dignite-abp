using System;
using Volo.Abp.Domain.Repositories;

namespace Dignite.Abp.UserPoints;
public interface IUserPointAccountRepository : IRepository<UserPointAccount, Guid>
{
}
