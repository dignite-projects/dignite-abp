using System;
using System.Collections.Generic;
using Dignite.Abp.FieldCustomizing.Fields;
using Dignite.Abp.FieldCustomizing.Fields.Select;
using Dignite.Abp.FieldCustomizing.Fields.Switch;
using Dignite.Abp.FieldCustomizing.Fields.Textbox;
using Volo.Abp.Settings;

namespace Dignite.Abp.SettingsGrouping;

public static class SettingDefinitionControlExtensions
{
    #region Setting definition field provider

    /// <summary>
    /// Get the provider of the setting field
    /// </summary>
    /// <param name="setting"></param>
    /// <returns></returns>
    public static string GetControlProviderNameOrNull(
        this SettingDefinition setting)
    {
        var providerName = setting.Properties.GetOrDefault(SettingDefinitionPropertiesNames.FieldControlProviderName);
        if (providerName != null)
        {
            return (string)providerName;
        }
        return null;
    }

    /// <summary>
    /// Get the configuration of the setting field
    /// </summary>
    /// <param name="setting"></param>
    /// <returns></returns>
    public static FieldConfigurationDictionary GetControlConfigurationOrNull(
        this SettingDefinition setting)
    {
        var controlConfiguration = setting.Properties.GetOrDefault(SettingDefinitionPropertiesNames.FieldControlConfigurationName);
        if (controlConfiguration == null)
            return null;
        else
            return (FieldConfigurationDictionary)controlConfiguration;
    }

    #endregion

    #region set controls configuration

    /// <summary>
    /// Use Textbox Control
    /// </summary>
    /// <param name="settingDefinition"></param>
    /// <param name="configureAction"></param>
    /// <returns></returns>
    public static SettingDefinition UseTextboxControl(
        this SettingDefinition settingDefinition,
        Action<TextboxConfiguration> configureAction)
    {
        var textboxConfiguration = new TextboxConfiguration(new FieldConfigurationDictionary());
        configureAction(textboxConfiguration);

        settingDefinition.WithProperty(SettingDefinitionPropertiesNames.FieldControlConfigurationName, textboxConfiguration.ConfigurationDictionary);
        settingDefinition.WithProperty(SettingDefinitionPropertiesNames.FieldControlProviderName, TextboxFieldProvider.ProviderName);

        return settingDefinition;
    }

    /// <summary>
    /// Use Switch Control
    /// </summary>
    /// <param name="settingDefinition"></param>
    /// <param name="configureAction"></param>
    /// <returns></returns>
    public static SettingDefinition UseSwitchControl(
        this SettingDefinition settingDefinition,
        Action<SwitchConfiguration> configureAction)
    {
        var configuration = new SwitchConfiguration(new FieldConfigurationDictionary());
        configureAction(configuration);

        settingDefinition.WithProperty(SettingDefinitionPropertiesNames.FieldControlConfigurationName, configuration.ConfigurationDictionary);
        settingDefinition.WithProperty(SettingDefinitionPropertiesNames.FieldControlProviderName, SwitchFieldProvider.ProviderName);

        return settingDefinition;
    }

    /// <summary>
    /// Use Select Control
    /// </summary>
    /// <param name="settingDefinition"></param>
    /// <param name="configureAction"></param>
    /// <returns></returns>
    public static SettingDefinition UseSelectControl(
        this SettingDefinition settingDefinition,
        Action<SelectConfiguration> configureAction)
    {
        var config = new SelectConfiguration(new FieldConfigurationDictionary());
        configureAction(config);

        settingDefinition.WithProperty(SettingDefinitionPropertiesNames.FieldControlConfigurationName, config.ConfigurationDictionary);
        settingDefinition.WithProperty(SettingDefinitionPropertiesNames.FieldControlProviderName, SelectFieldProvider.ProviderName);

        return settingDefinition;
    }
    #endregion
}
