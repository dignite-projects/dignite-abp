using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Dignite.Abp.MultiTenancyDomains.Host.Data;

public class MultiTenancyDomainsHostDbContextFactory : IDesignTimeDbContextFactory<MultiTenancyDomainsHostDbContext>
{
    public MultiTenancyDomainsHostDbContext CreateDbContext(string[] args)
    {
        MultiTenancyDomainsHostEfCoreEntityExtensionMappings.Configure();

        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<MultiTenancyDomainsHostDbContext>()
            .UseSqlServer(configuration.GetConnectionString("Default"));

        return new MultiTenancyDomainsHostDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
