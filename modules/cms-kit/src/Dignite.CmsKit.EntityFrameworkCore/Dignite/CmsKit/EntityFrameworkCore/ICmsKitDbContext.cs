using Dignite.CmsKit.Favourites;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Dignite.CmsKit.EntityFrameworkCore;

[ConnectionStringName(DigniteCmsKitDbProperties.ConnectionStringName)]
public interface ICmsKitDbContext : IEfCoreDbContext
{
    DbSet<Favourite> Favourites { get; }
}
