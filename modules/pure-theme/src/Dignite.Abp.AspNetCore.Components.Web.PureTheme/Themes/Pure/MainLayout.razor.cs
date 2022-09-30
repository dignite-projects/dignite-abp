using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Dignite.Abp.AspNetCore.Components.Web.PureTheme.Themes.Pure;
public partial class MainLayout: LayoutComponentBase
{
    private SideNav SideNav { get; set; }


    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            await InvokeAsync(StateHasChanged);
        }
    }
}
