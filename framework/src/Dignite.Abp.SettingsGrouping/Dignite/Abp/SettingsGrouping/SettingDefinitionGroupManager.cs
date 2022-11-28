using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Settings;

namespace Dignite.Abp.SettingsGrouping;

public class SettingDefinitionGroupManager : SettingDefinitionManager, ISettingDefinitionGroupManager, ISingletonDependency
{
    protected Lazy<IReadOnlyList<ISettingDefinitionGroupProvider>> AllProviders { get; }
    protected AbpSettingGroupOptions SettingGroupOptions { get; }

    public SettingDefinitionGroupManager(
        IOptions<AbpSettingGroupOptions> settingGroupOptions,
        IOptions<AbpSettingOptions> settingOptions,
        IServiceProvider serviceProvider)
        : base(settingOptions, serviceProvider)
    {
        SettingGroupOptions = settingGroupOptions.Value;
        AllProviders = new Lazy<IReadOnlyList<ISettingDefinitionGroupProvider>>(CreateSettingDefinitionGroups, true);
    }

    public virtual IReadOnlyList<SettingDefinitionGroup> GetAllGroups()
    {
        var groups = new List<SettingDefinitionGroup>();
        foreach (var provider in AllProviders.Value)
        {
            var settings = new Dictionary<string, SettingDefinition>();
            var group = provider.Group;
            provider.Define(new SettingDefinitionContext(settings));

            //
            group.SettingDefinitions = settings.Values
                .Where(s => s.GetGroupOrNull() == null)
                .ToList();

            //
            group.SubGroups = settings.Values
                .Where(s => s.GetGroupOrNull() != null)
                .Select(s => s.GetGroupOrNull())
                .Distinct()
                .ToList();

            //
            foreach (var subGroup in group.SubGroups)
            {
                subGroup.SettingDefinitions = settings.Values
                    .Where(s => s.GetGroupOrNull() == subGroup)
                    .ToList();
            }

            //
            groups.Add(group);
        }

        return groups;
    }

    protected virtual IReadOnlyList<ISettingDefinitionGroupProvider> CreateSettingDefinitionGroups()
    {
        using (var scope = ServiceProvider.CreateScope())
        {
            return SettingGroupOptions
                .DefinitionGroupProviders
                .Select(p => scope.ServiceProvider.GetRequiredService(p) as ISettingDefinitionGroupProvider)
                .ToList();
        }
    }

}