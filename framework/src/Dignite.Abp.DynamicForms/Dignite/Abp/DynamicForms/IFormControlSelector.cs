using JetBrains.Annotations;

namespace Dignite.Abp.DynamicForms;

public interface IFormControlSelector
{
    /// <summary>
    /// Get form control using name
    /// </summary>
    /// <param name="formControlName">
    /// The <see cref="IFormControl.Name"/>
    /// </param>
    /// <returns></returns>
    [NotNull]
    IFormControl Get(string formControlName);
}