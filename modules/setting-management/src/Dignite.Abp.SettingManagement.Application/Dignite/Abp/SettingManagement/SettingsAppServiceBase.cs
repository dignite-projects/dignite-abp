using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Dignite.Abp.Settings.DynamicForms;
using Volo.Abp.Application.Dtos;
using Volo.Abp.SettingManagement;
using Volo.Abp.Settings;

namespace Dignite.Abp.SettingManagement;

public abstract class SettingsAppServiceBase : SettingManagementAppServiceBase
{
    protected ISettingDefinitionFormManager SettingDefinitionManager { get; }
    protected ISettingManager SettingManager { get; }

    protected SettingsAppServiceBase(
        ISettingDefinitionFormManager settingDefinitionManager,
        ISettingManager settingManager)
    {
        SettingDefinitionManager = settingDefinitionManager;
        SettingManager = settingManager;
    }

    public async Task<ListResultDto<SettingProviderDto>> GetAllAsync()
    {
        var settingDefinitionProviders = SettingDefinitionManager.GetProviders();
        var settingValues = await GetSettingValues();
        var dto = new List<SettingProviderDto>();
        foreach (var provider in settingDefinitionProviders)
        {
            var settingDefinitions = SettingDefinitionManager.GetList(provider)
                                                            .Where(sd =>sd.GetFormConfigurationOrNull() != null)
                                                            .ToList();
            if (settingDefinitions.Any())
            {
                var settings = new List<SettingDto>();
                foreach (var definition in settingDefinitions)
                {
                    var value = settingValues.FirstOrDefault(sv => sv.Name == definition.Name)?.Value;
                    var group = definition.GetGroupOrNull();
                    settings.Add(
                        new SettingDto(
                            definition.Name,
                            definition.DisplayName == null ? null : definition.DisplayName.Localize(StringLocalizerFactory),
                            definition.GetFormProviderNameOrNull(),
                            definition.GetFormConfigurationOrNull(),
                            group == null ? null : group.Localize(StringLocalizerFactory),
                            definition.Description == null ? null : definition.Description.Localize(StringLocalizerFactory),
                            value
                        ));
                }

                dto.Add(new SettingProviderDto(
                    SettingDefinitionProviderNameAttribute.GetProviderName(provider.GetType()),
                    provider.DisplayName.Localize(StringLocalizerFactory),
                    settings
                    ));
            }
        }

        return
            new ListResultDto<SettingProviderDto>(dto);
    }

    protected async Task UpdateAsync(UpdateSettingsInput input)
    {
        var settingDefinitions = SettingDefinitionManager.GetList(input.ProviderName);
        var settings = input.CustomFields.Where(s =>
            settingDefinitions.Select(sd => sd.Name).Contains(s.Key)
        );

        foreach (var setting in settings)
        {
            await UpdateAsync(setting.Key, setting.Value.ToString());
        }
    }

    protected abstract Task<List<SettingValue>> GetSettingValues();

    protected abstract Task UpdateAsync(string name, string value);
}