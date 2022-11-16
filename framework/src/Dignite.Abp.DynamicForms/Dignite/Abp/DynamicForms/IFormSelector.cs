using JetBrains.Annotations;

namespace Dignite.Abp.DynamicForms;

public interface IFormSelector
{
    /// <summary>
    /// Get form using name
    /// </summary>
    /// <param name="formName">
    /// <see cref="IForm.Name"/>
    /// </param>
    /// <returns></returns>
    [NotNull]
    IForm Get(string formName);
}