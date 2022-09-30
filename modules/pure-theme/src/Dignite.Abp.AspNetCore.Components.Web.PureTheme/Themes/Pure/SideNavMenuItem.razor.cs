using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Volo.Abp.UI.Navigation;

namespace Dignite.Abp.AspNetCore.Components.Web.PureTheme.Themes.Pure;
public partial class SideNavMenuItem
{
    [Inject] private NavigationManager NavigationManager { get; set; }

    [Parameter]
    public ApplicationMenuItem MenuItem { get; set; }

    protected override void OnInitialized()
    {
        NavigationManager.LocationChanged += OnLocationChanged;
    }

    public void Dispose()
    {
        NavigationManager.LocationChanged -= OnLocationChanged;
    }

    private void OnLocationChanged(object sender, LocationChangedEventArgs e)
    {
        InvokeAsync(StateHasChanged);
    }
}
