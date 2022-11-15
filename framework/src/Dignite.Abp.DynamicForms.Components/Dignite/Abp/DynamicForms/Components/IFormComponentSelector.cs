using JetBrains.Annotations;

namespace Dignite.Abp.DynamicForms.Components;

public interface IFormComponentSelector
{
    /// <summary>
    /// Get form control component using field form provider name
    /// </summary>
    /// <param name="formProviderName">
    /// <see cref="IFormProvider.Name"/>
    /// </param>
    /// <returns></returns>
    [NotNull]
    IFormComponent Get(string formProviderName);
}