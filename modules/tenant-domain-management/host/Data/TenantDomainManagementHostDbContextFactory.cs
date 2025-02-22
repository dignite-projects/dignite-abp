using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Dignite.Abp.TenantDomainManagement.Host.Data;

public class TenantDomainManagementHostDbContextFactory : IDesignTimeDbContextFactory<TenantDomainManagementHostDbContext>
{
    public TenantDomainManagementHostDbContext CreateDbContext(string[] args)
    {
        TenantDomainManagementHostEfCoreEntityExtensionMappings.Configure();

        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<TenantDomainManagementHostDbContext>()
            .UseSqlServer(configuration.GetConnectionString("Default"));

        return new TenantDomainManagementHostDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
