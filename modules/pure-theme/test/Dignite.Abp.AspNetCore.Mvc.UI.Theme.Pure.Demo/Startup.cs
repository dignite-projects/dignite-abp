using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Dignite.Abp.AspNetCore.Mvc.UI.Theme.Pure.Demo;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddApplication<AbpAspNetCoreMvcUiThemePureDemoModule>();
    }

    public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
    {
        app.InitializeApplication();
    }
}
