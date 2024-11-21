using Dignite.CmsKit.Visits;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.CmsKit;

namespace Dignite.CmsKit.EntityFrameworkCore;

[ConnectionStringName(AbpCmsKitDbProperties.ConnectionStringName)]
public interface ICmsKitDbContext : IEfCoreDbContext
{
    DbSet<Visit> Visits { get; }
}
