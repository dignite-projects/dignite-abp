using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Localization.Resources.AbpUi;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.JSInterop;
using Volo.Abp.UI.Navigation;

namespace Dignite.Abp.AspNetCore.Components.WebAssembly.PureTheme.Themes.Pure
{
    public partial class LoginDisplay : IDisposable
    {
        [Inject]
        protected IMenuManager MenuManager { get; set; }

        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [CanBeNull]
        protected SignOutSessionStateManager SignOutManager;

        public LoginDisplay()
        {
            LocalizationResource = typeof(AbpUiResource);
        }

        protected ApplicationMenu Menu { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Menu = await MenuManager.GetAsync(StandardMenus.User);

            Navigation.LocationChanged += OnLocationChanged;

            LazyGetService(ref SignOutManager);

            AuthenticationStateProvider.AuthenticationStateChanged +=
                AuthenticationStateProviderOnAuthenticationStateChanged;
        }

        protected virtual void OnLocationChanged(object sender, LocationChangedEventArgs e)
        {
            InvokeAsync(StateHasChanged);
        }

        private async void AuthenticationStateProviderOnAuthenticationStateChanged(Task<AuthenticationState> task)
        {
            Menu = await MenuManager.GetAsync(StandardMenus.User);
            await InvokeAsync(StateHasChanged);
        }

        public void Dispose()
        {
            Navigation.LocationChanged -= OnLocationChanged;
            AuthenticationStateProvider.AuthenticationStateChanged -=
                AuthenticationStateProviderOnAuthenticationStateChanged;
        }

        private async Task NavigateToAsync(string uri, string target = null)
        {
            if (target == "_blank")
            {
                await JsRuntime.InvokeVoidAsync("open", uri, target);
            }
            else
            {
                Navigation.NavigateTo(uri);
            }
        }

        private async Task BeginSignOut()
        {
            if (SignOutManager != null)
            {
                await SignOutManager.SetSignOutState();
                await NavigateToAsync("authentication/logout");
            }
        }
    }
}
