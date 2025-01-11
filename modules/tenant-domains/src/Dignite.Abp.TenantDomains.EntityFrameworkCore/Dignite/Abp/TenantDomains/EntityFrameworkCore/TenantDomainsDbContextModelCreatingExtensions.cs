﻿using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Dignite.Abp.TenantDomains.EntityFrameworkCore;

public static class TenantDomainsDbContextModelCreatingExtensions
{
    public static void ConfigureTenantDomains(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));


        builder.Entity<TenantDomain>(domain =>
        {
            //Configure table & schema name
            domain.ToTable(TenantDomainsDbProperties.DbTablePrefix + "TenantDomains", TenantDomainsDbProperties.DbSchema);

            domain.ConfigureByConvention();

            //Properties
            domain.Property(s => s.DomainName).IsRequired().HasMaxLength(TenantDomainConsts.MaxDomainNameLength);
            //Indexs
            domain.HasIndex(s => s.DomainName);
        });
    }
}
