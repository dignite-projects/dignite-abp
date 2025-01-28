using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Dignite.Cms.EntityFrameworkCore;

[ConnectionStringName(CmsDbProperties.ConnectionStringName)]
public interface ICmsDbContext : IEfCoreDbContext
{
    DbSet<Sections.Section> Sections { get; }
    DbSet<Sections.EntryType> EntryTypes { get; }
    DbSet<Fields.FieldGroup> FieldGroups { get; }
    DbSet<Fields.Field> Fields { get; }
    DbSet<Entries.Entry> Entries { get; }
}
