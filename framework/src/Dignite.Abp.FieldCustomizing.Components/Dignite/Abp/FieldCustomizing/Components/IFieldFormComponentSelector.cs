using Dignite.Abp.FieldCustomizing.Forms;
using JetBrains.Annotations;

namespace Dignite.Abp.FieldCustomizing.Components;

public interface IFieldFormComponentSelector
{
    /// <summary>
    /// Get form control component using field form provider name
    /// </summary>
    /// <param name="formProviderName">
    /// <see cref="IFormProvider.Name"/>
    /// </param>
    /// <returns></returns>
    [NotNull]
    IFieldFormComponent Get(string formProviderName);
}