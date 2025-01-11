using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Dignite.Abp.RegionalizationManagement.Host.Data;

public class RegionalizationManagementDbContextFactory : IDesignTimeDbContextFactory<RegionalizationManagementDbContext>
{
    public RegionalizationManagementDbContext CreateDbContext(string[] args)
    {
        RegionalizationManagementEfCoreEntityExtensionMappings.Configure();

        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<RegionalizationManagementDbContext>()
            .UseSqlServer(configuration.GetConnectionString("Default"));

        return new RegionalizationManagementDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
