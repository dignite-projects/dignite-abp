﻿using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Dignite.Abp.UserPoints.EntityFrameworkCore;

[ConnectionStringName(UserPointsDbProperties.ConnectionStringName)]
public class UserPointsDbContext : AbpDbContext<UserPointsDbContext>, IUserPointsDbContext
{
    public DbSet<UserPointsItem> UserPointsItems { get; set; }
    public DbSet<UserPointsOrder> UserPointsOrders { get; set; }
    public DbSet<UserPointsBlock> UserPointsBlocks { get; set; }

    public UserPointsDbContext(DbContextOptions<UserPointsDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureUserPoints();
    }
}
