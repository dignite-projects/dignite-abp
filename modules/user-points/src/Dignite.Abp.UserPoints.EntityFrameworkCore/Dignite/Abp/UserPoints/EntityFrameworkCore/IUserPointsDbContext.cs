using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Dignite.Abp.UserPoints.EntityFrameworkCore;

[ConnectionStringName(UserPointsDbProperties.ConnectionStringName)]
public interface IUserPointsDbContext : IEfCoreDbContext
{
    DbSet<UserPoint> UserPoints { get; }
}
