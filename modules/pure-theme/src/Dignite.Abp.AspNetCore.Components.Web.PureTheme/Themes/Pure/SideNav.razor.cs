using System;
using System.Threading.Tasks;
using Blazorise;
using Blazorise.Modules;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.JSInterop;
using Volo.Abp.UI.Navigation;

namespace Dignite.Abp.AspNetCore.Components.Web.PureTheme.Themes.Pure;

public partial class SideNav : BaseComponent, IBreakpointActivator, IAsyncDisposable
{
    #region Members

    /// <summary>
    /// Used to keep track of the breakpoint state for this component.
    /// </summary>
    private bool isBroken = true;

    /// <summary>
    /// Reference to the object that should be accessed through JSInterop.
    /// </summary>
    private DotNetObjectReference<BreakpointActivatorAdapter> dotNetObjectRef;


    private BarCollapseMode collapseMode = BarCollapseMode.Hide;
    protected ApplicationMenu Menu { get; set; }

    public Breakpoint Breakpoint { get; private set; } = Breakpoint.Tablet;
    #endregion

    #region Methods
    protected override async Task OnInitializedAsync()
    {
        Menu = await MenuManager.GetMainMenuAsync();
        AuthenticationStateProvider.AuthenticationStateChanged += AuthenticationStateProviderOnAuthenticationStateChanged;
        NavigationManager.LocationChanged += OnLocationChanged;

        await base.OnInitializedAsync();
    }

    /// <inheritdoc/>
    protected override async Task OnFirstAfterRenderAsync()
    {
        dotNetObjectRef ??= CreateDotNetObjectRef(new BreakpointActivatorAdapter(this));

        await JSBreakpointModule.RegisterBreakpoint(dotNetObjectRef, ElementId);

        // Check if we need to collapse the Bar based on the current screen width against the breakpoint defined for this component.
        // This needs to be run to set the initial state, RegisterBreakpointComponent and OnBreakpoint will handle
        // additional changes to responsive breakpoints from there.
        isBroken = BreakpointActivatorAdapter.IsBroken(Breakpoint, await JSBreakpointModule.GetBreakpoint());

        if (isBroken)
            collapseMode = BarCollapseMode.Hide;
        else
        {
            collapseMode = BarCollapseMode.Small;
            await InvokeAsync(StateHasChanged);
        }


        await base.OnFirstAfterRenderAsync();
    }

    /// <inheritdoc/>
    public Task OnBreakpoint(bool broken)
    {
        // If the breakpoint state has changed, we need to toggle the visibility of this component.
        // broken = true, hide the component
        // broken = false, show the component
        if (isBroken == broken)
            collapseMode = BarCollapseMode.Hide;
        else
            collapseMode = BarCollapseMode.Small;

        return InvokeAsync(StateHasChanged);
    }


    /// <inheritdoc/>
    protected override async ValueTask DisposeAsync(bool disposing)
    {
        if (disposing)
        {
            // make sure to unregister listener
            if (Rendered)
            {
                var task = JSBreakpointModule.UnregisterBreakpoint(this);

                try
                {
                    await task;
                }
                catch when (task.IsCanceled)
                {
                }
                catch (Microsoft.JSInterop.JSDisconnectedException)
                {
                }

                DisposeDotNetObjectRef(dotNetObjectRef);
                dotNetObjectRef = null;
            }

            NavigationManager.LocationChanged -= OnLocationChanged;
            AuthenticationStateProvider.AuthenticationStateChanged -= AuthenticationStateProviderOnAuthenticationStateChanged;
        }

        await base.DisposeAsync(disposing);
    }


    /// <summary>
    /// An event that fires when the navigation location has changed.
    /// </summary>
    /// <param name="sender">Object the fired the notification.</param>
    /// <param name="args">New location arguments.</param>
    private async void OnLocationChanged(object sender, LocationChangedEventArgs args)
    {
        // Collapse the bar automatically
        if (BreakpointActivatorAdapter.IsBroken(Breakpoint, await JSBreakpointModule.GetBreakpoint()))
            collapseMode = BarCollapseMode.Hide;
        else
            collapseMode = BarCollapseMode.Small;

        await InvokeAsync(StateHasChanged);
    }


    private async void AuthenticationStateProviderOnAuthenticationStateChanged(Task<AuthenticationState> task)
    {
        Menu = await MenuManager.GetMainMenuAsync();
        await InvokeAsync(StateHasChanged);
    }
    #endregion

    /// <summary>
    /// 
    /// </summary>
    public Bar Sidebar { get; private set; }

    [Inject]
    protected IMenuManager MenuManager { get; set; }

    [Inject]
    protected AuthenticationStateProvider AuthenticationStateProvider { get; set; }


    /// <summary>
    /// Gets or sets the <see cref="IJSBreakpointModule"/> instance.
    /// </summary>
    [Inject] public IJSBreakpointModule JSBreakpointModule { get; set; }

    /// <summary>
    /// Injects the navigation manager.
    /// </summary>
    [Inject] protected NavigationManager NavigationManager { get; set; }

}