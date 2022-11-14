using System;
using System.Collections.Generic;
using Dignite.Abp.FieldCustomizing.Forms;
using Dignite.Abp.FieldCustomizing.Forms.Select;
using Dignite.Abp.FieldCustomizing.Forms.Switch;
using Dignite.Abp.FieldCustomizing.Forms.Textbox;
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
    public static FormConfigurationDictionary GetControlConfigurationOrNull(
        this SettingDefinition setting)
    {
        var controlConfiguration = setting.Properties.GetOrDefault(SettingDefinitionPropertiesNames.FieldControlConfigurationName);
        if (controlConfiguration == null)
            return null;
        else
            return (FormConfigurationDictionary)controlConfiguration;
    }

    #endregion Setting definition field provider

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
        var textboxConfiguration = new TextboxConfiguration(new FormConfigurationDictionary());
        configureAction(textboxConfiguration);

        settingDefinition.WithProperty(SettingDefinitionPropertiesNames.FieldControlConfigurationName, textboxConfiguration.ConfigurationDictionary);
        settingDefinition.WithProperty(SettingDefinitionPropertiesNames.FieldControlProviderName, TextboxFormProvider.ProviderName);

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
        var configuration = new SwitchConfiguration(new FormConfigurationDictionary());
        configureAction(configuration);

        settingDefinition.WithProperty(SettingDefinitionPropertiesNames.FieldControlConfigurationName, configuration.ConfigurationDictionary);
        settingDefinition.WithProperty(SettingDefinitionPropertiesNames.FieldControlProviderName, SwitchFormProvider.ProviderName);

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
        var config = new SelectConfiguration(new FormConfigurationDictionary());
        configureAction(config);

        settingDefinition.WithProperty(SettingDefinitionPropertiesNames.FieldControlConfigurationName, config.ConfigurationDictionary);
        settingDefinition.WithProperty(SettingDefinitionPropertiesNames.FieldControlProviderName, SelectFormProvider.ProviderName);

        return settingDefinition;
    }

    #endregion set controls configuration
}