using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Localization;
using Volo.Abp.Localization;

namespace Dignite.Abp.TenantLocalization;

    public static class TenantLocalizationServiceCollectionExtensions
{
        public static IServiceCollection AddTenantLocalization(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddOptions();

            AddJsonLocalizationServices(services);

            return services;
        }

        public static IServiceCollection AddTenantLocalization(
           this IServiceCollection services,
           Action<My.Extensions.Localization.Json.JsonLocalizationOptions> setupAction)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (setupAction == null)
            {
                throw new ArgumentNullException(nameof(setupAction));
            }
            
            AddJsonLocalizationServices(services, setupAction);

            return services;
        }

        internal static void AddJsonLocalizationServices(IServiceCollection services)
        {
            services.TryAddSingleton(typeof(AbpStringLocalizerFactory), typeof(AbpStringLocalizerFactory));
            services.TryAddSingleton(typeof(TenantJsonStringLocalizerFactory), typeof(TenantJsonStringLocalizerFactory));
            services.Replace(ServiceDescriptor.Singleton<IStringLocalizerFactory, TenantStringLocalizerFactory>());
            services.AddSingleton<ResourceManagerStringLocalizerFactory>();
            services.TryAddTransient(typeof(IStringLocalizer<>), typeof(StringLocalizer<>));
        }

        internal static void AddJsonLocalizationServices(
            IServiceCollection services,
            Action<My.Extensions.Localization.Json.JsonLocalizationOptions> setupAction)
        {
            AddJsonLocalizationServices(services);
            services.Configure(setupAction);
        }
    }
