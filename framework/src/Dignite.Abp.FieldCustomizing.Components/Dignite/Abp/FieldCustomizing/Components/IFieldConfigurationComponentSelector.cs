using Dignite.Abp.FieldCustomizing.Forms;
using JetBrains.Annotations;

namespace Dignite.Abp.FieldCustomizing.Components;

public interface IFieldConfigurationComponentSelector
{
    /// <summary>
    /// Get field configuration component using field form provider name
    /// </summary>
    /// <param name="formProviderName">
    /// <see cref="IFormProvider.Name"/>
    /// </param>
    /// <returns></returns>
    [NotNull]
    IFieldConfigurationComponent Get(string formProviderName);
}