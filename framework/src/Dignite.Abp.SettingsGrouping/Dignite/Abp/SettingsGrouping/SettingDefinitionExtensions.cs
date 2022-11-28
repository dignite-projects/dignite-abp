using System;
using System.Collections.Generic;
using Dignite.Abp.DynamicForms;
using Dignite.Abp.DynamicForms.Select;
using Dignite.Abp.DynamicForms.Switch;
using Dignite.Abp.DynamicForms.Textbox;
using Volo.Abp.Settings;

namespace Dignite.Abp.SettingsGrouping;

public static class SettingDefinitionExtensions
{
    /// <summary>
    /// Gets the group name of the setting
    /// </summary>
    /// <param name="setting"></param>
    /// <returns></returns>
    public static SettingDefinitionGroup GetGroupOrNull(
        this SettingDefinition setting)
    {
        var group = setting.Properties.GetOrDefault(SettingDefinitionPropertiesNames.Group);
        if (group != null)
        {
            return (SettingDefinitionGroup)group;
        }
        return null;
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="setting"></param>
    /// <param name="group"></param>
    public static SettingDefinition SetGroup(
        this SettingDefinition setting,
        SettingDefinitionGroup group)
    {
        setting.WithProperty(SettingDefinitionPropertiesNames.Group, group);
        return setting;
    }


    #region Setting definition dynamic form provider

    /// <summary>
    /// Get the provider name of the setting dynamic form
    /// </summary>
    /// <param name="setting"></param>
    /// <returns></returns>
    public static string GetDynamicFormProviderNameOrNull(
        this SettingDefinition setting)
    {
        var providerName = setting.Properties.GetOrDefault(SettingDefinitionPropertiesNames.DynamicFormProvider);
        if (providerName != null)
        {
            return (string)providerName;
        }
        return null;
    }

    /// <summary>
    /// Get the configuration of the setting dynamic form
    /// </summary>
    /// <param name="setting"></param>
    /// <returns></returns>
    public static FormConfigurationDictionary GetDynamicFormConfigurationOrNull(
        this SettingDefinition setting)
    {
        var formConfiguration = setting.Properties.GetOrDefault(SettingDefinitionPropertiesNames.DynamicFormConfiguration);
        if (formConfiguration == null)
            return null;
        else
            return (FormConfigurationDictionary)formConfiguration;
    }

    #endregion

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

        settingDefinition.WithProperty(SettingDefinitionPropertiesNames.DynamicFormConfiguration, textboxConfiguration.ConfigurationDictionary);
        settingDefinition.WithProperty(SettingDefinitionPropertiesNames.DynamicFormProvider, TextboxForm.TextboxFormName);

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

        settingDefinition.WithProperty(SettingDefinitionPropertiesNames.DynamicFormConfiguration, configuration.ConfigurationDictionary);
        settingDefinition.WithProperty(SettingDefinitionPropertiesNames.DynamicFormProvider, SwitchForm.SwitchFormName);

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

        settingDefinition.WithProperty(SettingDefinitionPropertiesNames.DynamicFormConfiguration, config.ConfigurationDictionary);
        settingDefinition.WithProperty(SettingDefinitionPropertiesNames.DynamicFormProvider, SelectForm.SwitchFormName);

        return settingDefinition;
    }

    #endregion
}