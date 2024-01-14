using JetBrains.Annotations;

namespace Dignite.Abp.DynamicForms.Components;

public interface IFormViewComponentSelector
{
    /// <summary>
    /// Get field component using field form control name
    /// </summary>
    /// <param name="formControlName">
    /// <see cref="IFormControl.Name"/>
    /// </param>
    /// <returns></returns>
    [NotNull]
    IFormViewComponent Get(string formControlName);
}