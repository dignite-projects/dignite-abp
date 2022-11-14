using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Dignite.Abp.FieldCustomizing.Forms;
using Dignite.Abp.SettingsGrouping;
using Volo.Abp.Application.Dtos;
using Volo.Abp.SettingManagement;
using Volo.Abp.Settings;

namespace Dignite.Abp.SettingManagement;

public abstract class SettingsAppServiceBase : SettingManagementAppServiceBase
{
    protected ISettingDefinitionGroupManager SettingDefinitionManager { get; }
    protected ISettingManager SettingManager { get; }
    protected IEnumerable<IFormProvider> FormProviders { get; }

    protected SettingsAppServiceBase(
        ISettingDefinitionGroupManager settingDefinitionManager,
        ISettingManager settingManager,
        IEnumerable<IFormProvider> formProviders)
    {
        SettingDefinitionManager = settingDefinitionManager;
        SettingManager = settingManager;
        FormProviders = formProviders;
    }

    public async Task<ListResultDto<SettingGroupDto>> GetAllAsync()
    {
        var navigations = SettingDefinitionManager.GetGroups();
        var settingValues = await GetSettingValues();
        var navigationList = new List<SettingGroupDto>();
        foreach (var nav in navigations)
        {
            var settingDefinitions = nav.SettingDefinitions.Where(sd =>
                sd.GetControlConfigurationOrNull() != null
                ).ToList();
            if (settingDefinitions.Any())
            {
                var settings = new List<SettingDto>();
                foreach (var sd in settingDefinitions)
                {
                    var value = settingValues.Any(sv => sv.Name == sd.Name) ? settingValues.Single(sv => sv.Name == sd.Name).Value : null;
                    var group = sd.GetSectionOrNull();
                    settings.Add(new SettingDto(
                        group == null ? null : group.Localize(StringLocalizerFactory),
                        sd.Name,
                        sd.DisplayName.Localize(StringLocalizerFactory),
                        sd.Description == null ? null : sd.Description.Localize(StringLocalizerFactory),
                        value,
                        sd.GetControlProviderNameOrNull(),
                        sd.GetControlConfigurationOrNull()
                        ));
                }

                navigationList.Add(new SettingGroupDto(
                    nav.Name,
                    nav.DisplayName.Localize(StringLocalizerFactory),
                    settings
                    ));
            }
        }

        return
            new ListResultDto<SettingGroupDto>(navigationList);
    }

    protected async Task UpdateAsync(UpdateSettingsInput input)
    {
        var settingDefinitions = SettingDefinitionManager.GetList(input.GroupName);
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