using System;
using System.Text.RegularExpressions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Localization;
using Volo.Abp.Localization;

namespace Dignite.Abp.Localization;

public static class MultiTenancyLocalizationServiceCollectionExtensions
{
    public static IServiceCollection AddMultiTenancyLocalization(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddOptions();

        AddJsonLocalizationServices(services);

        return services;
    }

    public static IServiceCollection AddMultiTenancyLocalization(
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
        services.TryAddSingleton(typeof(MultiTenancyJsonStringLocalizerFactory), typeof(MultiTenancyJsonStringLocalizerFactory));
        services.Replace(ServiceDescriptor.Singleton<IStringLocalizerFactory, MultiTenancyStringLocalizerFactory>());
    }

    internal static void AddJsonLocalizationServices(
        IServiceCollection services,
        Action<My.Extensions.Localization.Json.JsonLocalizationOptions> setupAction)
    {
        AddJsonLocalizationServices(services);
        services.Configure(setupAction);
    }
}
