using JetBrains.Annotations;

namespace Dignite.Abp.DynamicForms.Components;

public interface IConfigurationComponentSelector
{
    /// <summary>
    /// Get field configuration component using form name
    /// </summary>
    /// <param name="formName">
    /// <see cref="IForm.Name"/>
    /// </param>
    /// <returns></returns>
    [NotNull]
    IConfigurationComponent Get(string formName);
}