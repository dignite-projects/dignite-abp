using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Settings;

namespace Dignite.Abp.Settings.DynamicForms;

public class SettingDefinitionFormManager : SettingDefinitionManager, ISettingDefinitionFormManager, ISingletonDependency
{
    protected Lazy<IReadOnlyList<ISettingDefinitionFormProvider>> AllProviders { get; }
    protected AbpSettingFormOptions GroupingSettingOptions { get; }

    public SettingDefinitionFormManager(
        IOptions<AbpSettingFormOptions> groupingSettingOptions,
        IOptions<AbpSettingOptions> settingOptions,
        IServiceProvider serviceProvider)
        : base(settingOptions, serviceProvider)
    {
        GroupingSettingOptions = groupingSettingOptions.Value;
        AllProviders = new Lazy<IReadOnlyList<ISettingDefinitionFormProvider>>(CreateSettingDefinitionGroups, true);
    }

    public virtual IReadOnlyList<ISettingDefinitionFormProvider> GetProviders()
    {
        return AllProviders.Value;
    }

    public virtual IReadOnlyList<SettingDefinition> GetList(string providerName)
    {
        Check.NotNull(providerName, nameof(providerName));

        var provider = AllProviders.Value.SingleOrDefault(p => SettingDefinitionProviderNameAttribute.GetProviderName(p.GetType()) == providerName);

        if (provider == null)
        {
            throw new AbpException("Undefined provider: " + providerName);
        }

        return GetList(provider);
    }

    public virtual IReadOnlyList<SettingDefinition> GetList(ISettingDefinitionFormProvider provider)
    {
        var settings = new Dictionary<string, SettingDefinition>();
        provider.Define(new SettingDefinitionContext(settings));
        return settings.Values.ToList();
    }

    protected virtual IReadOnlyList<ISettingDefinitionFormProvider> CreateSettingDefinitionGroups()
    {
        using (var scope = ServiceProvider.CreateScope())
        {
            return GroupingSettingOptions
                .GroupingDefinitionProviders
                .Select(p => scope.ServiceProvider.GetRequiredService(p) as ISettingDefinitionFormProvider)
                .ToList();
        }
    }

}