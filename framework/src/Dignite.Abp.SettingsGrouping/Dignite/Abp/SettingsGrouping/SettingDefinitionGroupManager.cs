using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
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

    public virtual IReadOnlyList<SettingDefinitionGroup> GetGroups()
    {
        return Groups.Value;
    }

    public virtual IReadOnlyList<SettingDefinitionSection> GetSections(string groupName)
    {
        var settings = GetList(groupName);
        var sectionNames = settings.GroupBy(s => s.GetSectionOrNull()).Select(s => s.Key).ToList();
        var sections = new List<SettingDefinitionSection>();
        foreach (var section in sectionNames)
        {
            sections.Add(
                new SettingDefinitionSection(
                    section,
                    GetList(groupName).Where(s => s.GetSectionOrNull() == section).ToArray()
                ));
        }

        return sections;
    }

    public virtual IReadOnlyList<SettingDefinition> GetList(string groupName)
    {
        Check.NotNull(groupName, nameof(groupName));

        var group = Groups.Value.SingleOrDefault(n => n.Name == groupName);

        if (group == null)
        {
            throw new AbpException("Undefined group: " + groupName);
        }

        return group.SettingDefinitions;
    }


    protected virtual IReadOnlyList<SettingDefinitionGroup> CreateSettingDefinitionGroups()
    {
        var groups = new List<SettingDefinitionGroup>();
        using (var scope = ServiceProvider.CreateScope())
        {
            var providers = GroupingSettingOptions
                .GroupingDefinitionProviders
                .Select(p => scope.ServiceProvider.GetRequiredService(p) as ISettingDefinitionGroupProvider)
                .ToList();

            foreach (var provider in providers)
            {
                var settings = new Dictionary<string, SettingDefinition>();
                var context = new SettingDefinitionGroupContext(settings);
                provider.Define(context);
                context.Group.IncludeSettingDefinitions(settings);
                groups.Add(context.Group);
            }
        }

        return groups;
    }
}
