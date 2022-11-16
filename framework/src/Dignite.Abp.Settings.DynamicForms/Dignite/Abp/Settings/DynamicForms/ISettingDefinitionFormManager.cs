using System.Collections.Generic;
using Volo.Abp.Settings;

namespace Dignite.Abp.Settings.DynamicForms;

public interface ISettingDefinitionFormManager : ISettingDefinitionManager
{
    /// <summary>
    /// Get all provider of the settings definition
    /// </summary>
    /// <returns></returns>
    IReadOnlyList<ISettingDefinitionFormProvider> GetProviders();

    /// <summary>
    /// Get setting definition list
    /// </summary>
    /// <param name="providerName"></param>
    /// <returns></returns>
    IReadOnlyList<SettingDefinition> GetList(string providerName);


    IReadOnlyList<SettingDefinition> GetList(ISettingDefinitionFormProvider provider);
}