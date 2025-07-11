using Dignite.Abp.AspNetCore.Mvc.UI.Theme.Pure.Themes.Pure.Components.Toolbar.LocaleSwitch;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared.Toolbars;

namespace PureTheme.BlazorServerSample.Toolbars;

public class BlazorServerSampleToolbarContributor : IToolbarContributor
{
    public Task ConfigureToolbarAsync(IToolbarConfigurationContext context)
    {
        if (context.Toolbar.Name == StandardToolbars.Main)
        {
            context.Toolbar.Items.RemoveAll(items=>items.ComponentType==typeof(LocaleSwitchViewComponent));
            return Task.CompletedTask;
        }

        if (!(context.Theme is Dignite.Abp.AspNetCore.Mvc.UI.Theme.Pure.PureTheme))
        {
            return Task.CompletedTask;
        }

        return Task.CompletedTask;
    }
}