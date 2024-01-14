using JetBrains.Annotations;

namespace Dignite.Abp.DynamicForms.Components;

public interface IFormConfigurationComponentSelector
{
    /// <summary>
    /// Get field configuration component using form control name
    /// </summary>
    /// <param name="formControlName">
    /// <see cref="IFormControl.Name"/>
    /// </param>
    /// <returns></returns>
    [NotNull]
    IFormConfigurationComponent Get(string formControlName);
}