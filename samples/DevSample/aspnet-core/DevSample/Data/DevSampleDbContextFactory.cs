using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DevSample.Data;

public class DevSampleDbContextFactory : IDesignTimeDbContextFactory<DevSampleDbContext>
{
    public DevSampleDbContext CreateDbContext(string[] args)
    {

        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<DevSampleDbContext>()
            .UseSqlServer(configuration.GetConnectionString("Default"));

        return new DevSampleDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
