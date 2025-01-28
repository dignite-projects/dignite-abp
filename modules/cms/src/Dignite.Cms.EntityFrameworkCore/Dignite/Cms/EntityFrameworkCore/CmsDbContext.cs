using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Dignite.Cms.EntityFrameworkCore;

[ConnectionStringName(CmsDbProperties.ConnectionStringName)]
public class CmsDbContext : AbpDbContext<CmsDbContext>, ICmsDbContext
{
    public DbSet<Sections.Section> Sections { get; set; }
    public DbSet<Sections.EntryType> EntryTypes { get; set; }
    public DbSet<Fields.FieldGroup> FieldGroups { get; set; }
    public DbSet<Fields.Field> Fields { get; set; }
    public DbSet<Entries.Entry> Entries { get; set; }

    public CmsDbContext(DbContextOptions<CmsDbContext> options)
        : base(options)
    {

    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //Output SQL statements through logs
        optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information)
            .EnableSensitiveDataLogging();
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ConfigureCms();
    }
}
