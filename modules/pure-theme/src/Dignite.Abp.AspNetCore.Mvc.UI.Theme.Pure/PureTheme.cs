using Dignite.Abp.AspNetCore.Mvc.UI.TenantTheme;
using Dignite.Abp.AspNetCore.Mvc.UI.Theme.Pure.Themes.Pure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Volo.Abp.AspNetCore.Mvc.UI.Theming;
using Volo.Abp.DependencyInjection;
using Volo.Abp.MultiTenancy;

namespace Dignite.Abp.AspNetCore.Mvc.UI.Theme.Pure;

[ThemeName(Name)]
public class PureTheme : TenantThemeBase, ITheme, ITransientDependency
{
    public const string Name = "Pure";

    private readonly IConfiguration _configuration;
    private readonly PureThemeMvcOptions _options;
    public PureTheme(IConfiguration configuration,
        IOptions<PureThemeMvcOptions> options,
        ICurrentTenant currentTenant, 
        IThemeSelector themeSelector) : base(currentTenant,themeSelector)
    {
        _configuration = configuration;
        _options = options.Value;
    }

    public override string GetLayout(string name, bool fallbackToDefault = true)
    {
        switch (name)
        {
            case StandardLayouts.Application:
                name = GetLayoutFromConfig(StandardLayouts.Application) ?? $"{StandardLayouts.Application}/{_options.ApplicationLayout}";
                break;
            case StandardLayouts.Account:
                name = GetLayoutFromConfig("Account") ?? $"{StandardLayouts.Account}/default";
                break;
            case StandardLayouts.Public:
                name = GetLayoutFromConfig("Public") ?? $"{StandardLayouts.Application}/{_options.ApplicationLayout}"; // No-public layout yet
                break;
            case StandardLayouts.Empty:
                name = GetLayoutFromConfig("Empty") ?? $"{StandardLayouts.Empty}/default";
                break;
            default:
                name = fallbackToDefault ? name : null;
                break;
        }

        return base.GetLayout(name, fallbackToDefault);
    }
    private string GetLayoutFromConfig(string layoutName)
    {
        return _configuration["PureTheme:Layouts:" + layoutName];
    }
}