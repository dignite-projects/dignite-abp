using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Dignite.Cms.EntityFrameworkCore;

public class CmsHttpApiHostMigrationsDbContextFactory : IDesignTimeDbContextFactory<CmsHttpApiHostMigrationsDbContext>
{
    public CmsHttpApiHostMigrationsDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<CmsHttpApiHostMigrationsDbContext>()
            .UseSqlServer(configuration.GetConnectionString("Cms"));

        return new CmsHttpApiHostMigrationsDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
