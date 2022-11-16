using JetBrains.Annotations;

namespace Dignite.Abp.DynamicForms.Components;

public interface IFieldComponentSelector
{
    /// <summary>
    /// Get field component using field form name
    /// </summary>
    /// <param name="formName">
    /// <see cref="IForm.Name"/>
    /// </param>
    /// <returns></returns>
    [NotNull]
    IFieldComponent Get(string formName);
}