using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Dignite.Abp.LocaleManagement.Host.Data;

public class LocaleManagementHostDbContextFactory : IDesignTimeDbContextFactory<LocaleManagementHostDbContext>
{
    public LocaleManagementHostDbContext CreateDbContext(string[] args)
    {
        LocaleManagementHostEfCoreEntityExtensionMappings.Configure();

        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<LocaleManagementHostDbContext>()
            .UseSqlServer(configuration.GetConnectionString("Default"));

        return new LocaleManagementHostDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
