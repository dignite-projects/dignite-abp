using Dignite.Abp.FieldCustomizing.Forms;
using JetBrains.Annotations;

namespace Dignite.Abp.FieldCustomizing.Components;

public interface IFieldComponentSelector
{
    /// <summary>
    /// Get field component using field form provider name
    /// </summary>
    /// <param name="formProviderName">
    /// <see cref="IFormProvider.Name"/>
    /// </param>
    /// <returns></returns>
    [NotNull]
    IFieldComponent Get(string formProviderName);
}