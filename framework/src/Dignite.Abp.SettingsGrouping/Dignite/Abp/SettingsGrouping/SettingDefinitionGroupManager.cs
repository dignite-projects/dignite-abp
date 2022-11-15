using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Localization;
using Volo.Abp.Settings;

namespace Dignite.Abp.SettingsGrouping;

public class SettingDefinitionGroupManager : SettingDefinitionManager, ISettingDefinitionGroupManager, ISingletonDependency
{
    protected Lazy<IReadOnlyList<SettingDefinitionGroup>> Groups { get; }
    protected AbpSettingGroupOptions GroupingSettingOptions { get; }

    public SettingDefinitionGroupManager(
        IOptions<AbpSettingGroupOptions> groupingSettingOptions,
        IOptions<AbpSettingOptions> settingOptions,
        IServiceProvider serviceProvider)
        : base(settingOptions, serviceProvider)
    {
        GroupingSettingOptions = groupingSettingOptions.Value;
        Groups = new Lazy<IReadOnlyList<SettingDefinitionGroup>>(CreateSettingDefinitionGroups, true);
    }

    public virtual IReadOnlyList<string> GetGroups()
    {
        return Groups.Value.Select(g=>g.Name).ToList();
    }

    public virtual IReadOnlyList<ILocalizableString> GetSections(string groupName)
    {
        var settings = GetList(groupName);
        var sectionNames = settings.GroupBy(s => s.GetSectionOrNull()).Select(s => s.Key).ToList();
        return sectionNames;
    }

    public virtual IReadOnlyList<SettingDefinition> GetList(string groupName)
    {
        Check.NotNull(groupName, nameof(groupName));

        var group = Groups.Value.SingleOrDefault(n => n.Name == groupName);

        if (group == null)
        {
            throw new AbpException("Undefined group: " + groupName);
        }

        return group.Definitions.Values.ToList();
    }

    protected virtual IReadOnlyList<SettingDefinitionGroup> CreateSettingDefinitionGroups()
    {
        List<SettingDefinitionGroup> groups = new List<SettingDefinitionGroup>();

        using (var scope = ServiceProvider.CreateScope())
        {
            var providers = GroupingSettingOptions
                .GroupingDefinitionProviders
                .Select(p => scope.ServiceProvider.GetRequiredService(p) as ISettingDefinitionGroupProvider)
                .ToList();

            foreach (var provider in providers)
            {
                var settings = new Dictionary<string, SettingDefinition>();
                provider.Define(new SettingDefinitionContext(settings));
                groups.Add(new SettingDefinitionGroup(
                    provider.Section,settings
                    ));
            }
        }

        return groups;

    }
}