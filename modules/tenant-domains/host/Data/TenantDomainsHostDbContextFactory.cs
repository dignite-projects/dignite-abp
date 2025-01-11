using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Dignite.Abp.TenantDomains.Host.Data;

public class TenantDomainsHostDbContextFactory : IDesignTimeDbContextFactory<TenantDomainsHostDbContext>
{
    public TenantDomainsHostDbContext CreateDbContext(string[] args)
    {
        TenantDomainsHostEfCoreEntityExtensionMappings.Configure();

        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<TenantDomainsHostDbContext>()
            .UseSqlServer(configuration.GetConnectionString("Default"));

        return new TenantDomainsHostDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
