using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Localization;

namespace Dignite.Abp.TenantLocalization;

public static class TenantLocalizationServiceCollectionExtensions
{
    public static IServiceCollection AddTenantLocalization(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddOptions();

        AddJsonLocalizationServices(services);

        return services;
    }

    public static IServiceCollection AddTenantLocalization(
       this IServiceCollection services,
       Action<My.Extensions.Localization.Json.JsonLocalizationOptions> setupAction)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(setupAction);

        AddJsonLocalizationServices(services, setupAction);

        return services;
    }

    internal static void AddJsonLocalizationServices(IServiceCollection services)
    {
        services.TryAddSingleton(typeof(TenantJsonStringLocalizerFactory), typeof(TenantJsonStringLocalizerFactory));
        services.Replace(ServiceDescriptor.Singleton<IStringLocalizerFactory, TenantStringLocalizerFactory>());
    }

    internal static void AddJsonLocalizationServices(
        IServiceCollection services,
        Action<My.Extensions.Localization.Json.JsonLocalizationOptions> setupAction)
    {
        AddJsonLocalizationServices(services);
        services.Configure(setupAction);
    }
}
