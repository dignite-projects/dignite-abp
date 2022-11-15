using JetBrains.Annotations;

namespace Dignite.Abp.DynamicForms.Components;

public interface IConfigurationComponentSelector
{
    /// <summary>
    /// Get field configuration component using field form provider name
    /// </summary>
    /// <param name="formProviderName">
    /// <see cref="IFormProvider.Name"/>
    /// </param>
    /// <returns></returns>
    [NotNull]
    IConfigurationComponent Get(string formProviderName);
}