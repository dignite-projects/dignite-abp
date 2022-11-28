using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Dignite.Abp.SettingsGrouping;
using Volo.Abp.Application.Dtos;
using Volo.Abp.SettingManagement;
using Volo.Abp.Settings;

namespace Dignite.Abp.SettingManagement;

public abstract class SettingsAppServiceBase : SettingManagementAppServiceBase
{
    protected ISettingDefinitionGroupManager SettingDefinitionManager { get; }
    protected ISettingManager SettingManager { get; }

    protected SettingsAppServiceBase(
        ISettingDefinitionGroupManager settingDefinitionManager,
        ISettingManager settingManager)
    {
        SettingDefinitionManager = settingDefinitionManager;
        SettingManager = settingManager;
    }

    public async Task<ListResultDto<SettingGroupDto>> GetAllGroupsAsync()
    {
        var allSettings = await GetAllAsync();
        var settingDefinitionGroups = SettingDefinitionManager.GetAllGroups();
        var settingGroups = new List<SettingGroupDto>();
        foreach (var settingDefinitionGroup in settingDefinitionGroups)
        {
            var settingGroup = new SettingGroupDto(
                settingDefinitionGroup.Name,
                settingDefinitionGroup.DisplayName?.Localize(StringLocalizerFactory),
                settingDefinitionGroup.Description?.Localize(StringLocalizerFactory),
                settingDefinitionGroup.Icon
                );

            // setting sub group
            if (settingDefinitionGroup.SubGroups != null)
            {
                foreach (var subGroup in settingDefinitionGroup.SubGroups)
                {
                    var settingDefinitions = subGroup.SettingDefinitions
                                            .Where(sd => sd.GetDynamicFormProviderNameOrNull() != null && allSettings.Any(s => s.Name == sd.Name))
                                            .Select(sd => new SettingDto(
                                                sd.Name,
                                                sd.DisplayName?.Localize(StringLocalizerFactory),
                                                sd.GetDynamicFormProviderNameOrNull(),
                                                sd.GetDynamicFormConfigurationOrNull(),
                                                sd.Description?.Localize(StringLocalizerFactory),
                                                null
                                            )).ToList();
                    if (settingDefinitions.Any())
                    {
                        var settingSubGroup= new SettingGroupDto(
                                    subGroup.Name,
                                    subGroup.DisplayName?.Localize(StringLocalizerFactory),
                                    subGroup.Description?.Localize(StringLocalizerFactory),
                                    subGroup.Icon
                                    );
                        settingSubGroup.Settings = settingDefinitions;
                        settingGroup.SubGroups.Add(settingSubGroup);
                    }
                }
            }

            //
            if (settingDefinitionGroup.SettingDefinitions != null)
            {
                settingGroup.Settings = settingDefinitionGroup.SettingDefinitions
                    .Where(sd => sd.GetDynamicFormProviderNameOrNull() != null && allSettings.Any(s => s.Name == sd.Name))
                    .Select(sd => new SettingDto(
                        sd.Name,
                        sd.DisplayName?.Localize(StringLocalizerFactory),
                        sd.GetDynamicFormProviderNameOrNull(),
                        sd.GetDynamicFormConfigurationOrNull(),
                        sd.Description?.Localize(StringLocalizerFactory),
                        null
                    )).ToList();
            }

            //settingGroups
            if (settingGroup.SubGroups.Any()
                || (settingGroup.Settings.Any())
                )
            {
                settingGroups.Add(settingGroup);
            }
        }

        return new ListResultDto<SettingGroupDto>( settingGroups);
    }


    public async Task<ListResultDto<SettingDto>> GetListAsync(GetSettingsInput input)
    {
        var allSettings = await GetAllAsync();
        var groups = SettingDefinitionManager.GetAllGroups();
        IReadOnlyList<SettingDefinition> settingDefinitions = null;
        if (input.SubGroupName.IsNullOrEmpty())
        {
            settingDefinitions = groups.First(g => g.Name==input.GroupName).SettingDefinitions;
        }
        else
        {
            settingDefinitions = groups.First(g => g.Name == input.GroupName)
                .SubGroups.First(sg=>sg.Name== input.SubGroupName).SettingDefinitions;
        }

        return new ListResultDto<SettingDto>(
            settingDefinitions.Where(sd=> sd.GetDynamicFormProviderNameOrNull() != null && allSettings.Any(s=>s.Name== sd.Name))
            .Select(sd =>
                new SettingDto(
                    sd.Name,
                    sd.DisplayName?.Localize(StringLocalizerFactory),
                    sd.GetDynamicFormProviderNameOrNull(),
                    sd.GetDynamicFormConfigurationOrNull(),
                    sd.Description?.Localize(StringLocalizerFactory),
                    allSettings.FirstOrDefault(sv => sv.Name == sd.Name)?.Value
                )).ToList()
                );
    }

    protected async Task UpdateAsync(UpdateSettingsInput input)
    {
        var settingGroup = SettingDefinitionManager.GetAllGroups().First(g=>g.Name==input.GroupName);
        var settingDefinitions = input.SubGroupName.IsNullOrEmpty() ? 
            settingGroup.SettingDefinitions
            : settingGroup.SubGroups.First(g => g.Name==input.SubGroupName).SettingDefinitions;

        var settings = input.CustomFields.Where(s =>
            settingDefinitions.Select(sd => sd.Name).Contains(s.Key)
        );

        foreach (var setting in settings)
        {
            await UpdateAsync(setting.Key, setting.Value.ToString());
        }
    }

    protected abstract Task<List<SettingValue>> GetAllAsync();

    protected abstract Task UpdateAsync(string name, string value);
}