using Caddy.Client;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Volo.Abp.Modularity;

namespace Dignite.Abp;
public class DigniteCaddyModule: AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();
        Configure<CaddyOptions>(configuration.GetSection("Caddy"));

        context.Services.TryAddSingleton(service =>
        {
            var option = service.GetRequiredService<IOptions<CaddyOptions>>().Value;
            return new CaddyClient(option.Endpoint, option.Username, option.Password);
        });
    }
}
