using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Dignite.Abp.UserPoints.EntityFrameworkCore;

public class UserPointsHttpApiHostMigrationsDbContextFactory : IDesignTimeDbContextFactory<UserPointsHttpApiHostMigrationsDbContext>
{
    public UserPointsHttpApiHostMigrationsDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<UserPointsHttpApiHostMigrationsDbContext>()
            .UseSqlServer(configuration.GetConnectionString("UserPoints"));

        return new UserPointsHttpApiHostMigrationsDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
