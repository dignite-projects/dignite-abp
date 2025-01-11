using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Dignite.Abp.RegionalizationManagement.Host.Data;

public class RegionalizationManagementHostDbContextFactory : IDesignTimeDbContextFactory<RegionalizationManagementHostDbContext>
{
    public RegionalizationManagementHostDbContext CreateDbContext(string[] args)
    {
        RegionalizationManagementHostEfCoreEntityExtensionMappings.Configure();

        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<RegionalizationManagementHostDbContext>()
            .UseSqlServer(configuration.GetConnectionString("Default"));

        return new RegionalizationManagementHostDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
