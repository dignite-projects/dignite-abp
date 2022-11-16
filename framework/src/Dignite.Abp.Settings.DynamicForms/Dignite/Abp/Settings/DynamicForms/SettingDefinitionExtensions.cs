using System;
using System.Collections.Generic;
using Dignite.Abp.DynamicForms;
using Dignite.Abp.DynamicForms.Select;
using Dignite.Abp.DynamicForms.Switch;
using Dignite.Abp.DynamicForms.Textbox;
using Volo.Abp.Localization;
using Volo.Abp.Settings;

namespace Dignite.Abp.Settings.DynamicForms;

public static class SettingDefinitionExtensions
{
    /// <summary>
    /// Gets the group name of the setting
    /// </summary>
    /// <param name="setting"></param>
    /// <returns></returns>
    public static ILocalizableString GetGroupOrNull(
        this SettingDefinition setting)
    {
        var group = setting.Properties.GetOrDefault(SettingDefinitionPropertiesNames.Group);
        if (group != null)
        {
            return (ILocalizableString)group;
        }
        return null;
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="setting"></param>
    /// <param name="groupName"></param>
    public static SettingDefinition SetGroup(
        this SettingDefinition setting,
        ILocalizableString groupName)
    {
        setting.WithProperty(SettingDefinitionPropertiesNames.Group, groupName);
        return setting;
    }


    #region Setting definition field provider

    /// <summary>
    /// Get the provider name of the setting form
    /// </summary>
    /// <param name="setting"></param>
    /// <returns></returns>
    public static string GetFormProviderNameOrNull(
        this SettingDefinition setting)
    {
        var providerName = setting.Properties.GetOrDefault(SettingDefinitionPropertiesNames.FormProvider);
        if (providerName != null)
        {
            return (string)providerName;
        }
        return null;
    }

    /// <summary>
    /// Get the configuration of the setting form
    /// </summary>
    /// <param name="setting"></param>
    /// <returns></returns>
    public static FormConfigurationDictionary GetFormConfigurationOrNull(
        this SettingDefinition setting)
    {
        var formConfiguration = setting.Properties.GetOrDefault(SettingDefinitionPropertiesNames.FormConfiguration);
        if (formConfiguration == null)
            return null;
        else
            return (FormConfigurationDictionary)formConfiguration;
    }

    #endregion Setting definition field provider

    #region set dynamic form

    /// <summary>
    /// Use Textbox Control
    /// </summary>
    /// <param name="settingDefinition"></param>
    /// <param name="configureAction"></param>
    /// <returns></returns>
    public static SettingDefinition UseTextboxForm(
        this SettingDefinition settingDefinition,
        Action<TextboxConfiguration> configureAction)
    {
        var textboxConfiguration = new TextboxConfiguration(new FormConfigurationDictionary());
        configureAction(textboxConfiguration);

        settingDefinition.WithProperty(SettingDefinitionPropertiesNames.FormConfiguration, textboxConfiguration.ConfigurationDictionary);
        settingDefinition.WithProperty(SettingDefinitionPropertiesNames.FormProvider, TextboxForm.TextboxFormName);

        return settingDefinition;
    }

    /// <summary>
    /// Use Switch Control
    /// </summary>
    /// <param name="settingDefinition"></param>
    /// <param name="configureAction"></param>
    /// <returns></returns>
    public static SettingDefinition UseSwitchForm(
        this SettingDefinition settingDefinition,
        Action<SwitchConfiguration> configureAction)
    {
        var configuration = new SwitchConfiguration(new FormConfigurationDictionary());
        configureAction(configuration);

        settingDefinition.WithProperty(SettingDefinitionPropertiesNames.FormConfiguration, configuration.ConfigurationDictionary);
        settingDefinition.WithProperty(SettingDefinitionPropertiesNames.FormProvider, SwitchForm.SwitchFormName);

        return settingDefinition;
    }

    /// <summary>
    /// Use Select Control
    /// </summary>
    /// <param name="settingDefinition"></param>
    /// <param name="configureAction"></param>
    /// <returns></returns>
    public static SettingDefinition UseSelectForm(
        this SettingDefinition settingDefinition,
        Action<SelectConfiguration> configureAction)
    {
        var config = new SelectConfiguration(new FormConfigurationDictionary());
        configureAction(config);

        settingDefinition.WithProperty(SettingDefinitionPropertiesNames.FormConfiguration, config.ConfigurationDictionary);
        settingDefinition.WithProperty(SettingDefinitionPropertiesNames.FormProvider, SelectForm.SwitchFormName);

        return settingDefinition;
    }

    #endregion set controls configuration
}