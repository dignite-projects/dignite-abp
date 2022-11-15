using JetBrains.Annotations;

namespace Dignite.Abp.DynamicForms;

public interface IFormProviderSelector
{
    /// <summary>
    /// Get provider using field name
    /// </summary>
    /// <param name="providerName">
    /// <see cref="IFormProvider.Name"/>
    /// </param>
    /// <returns></returns>
    [NotNull]
    IFormProvider Get(string providerName);
}