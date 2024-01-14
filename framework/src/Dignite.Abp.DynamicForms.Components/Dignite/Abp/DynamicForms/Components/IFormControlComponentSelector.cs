using JetBrains.Annotations;

namespace Dignite.Abp.DynamicForms.Components;

public interface IFormControlComponentSelector
{
    /// <summary>
    /// Get form component using form name
    /// </summary>
    /// <param name="formControlName">
    /// <see cref="IFormControl.Name"/>
    /// </param>
    /// <returns></returns>
    [NotNull]
    IFormControlComponent Get(string formControlName);
}