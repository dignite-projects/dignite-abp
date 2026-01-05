using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Dignite.Abp.UserPoints.EntityFrameworkCore;
public class EfCoreUserPointAccountRepository : EfCoreRepository<IUserPointsDbContext, UserPointAccount, Guid>, IUserPointAccountRepository
{
    public EfCoreUserPointAccountRepository(IDbContextProvider<IUserPointsDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }
}
