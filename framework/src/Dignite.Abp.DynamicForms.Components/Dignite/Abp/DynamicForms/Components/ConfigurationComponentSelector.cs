using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.DynamicForms.Components;

public class ConfigurationComponentSelector : IConfigurationComponentSelector, ITransientDependency
{
    private readonly IEnumerable<IConfigurationComponent> _formConfigurationComponents;
    private readonly IFormSelector _formSelector;

    public ConfigurationComponentSelector(IEnumerable<IConfigurationComponent> formConfigurationComponents, IFormSelector formSelector)
    {
        _formConfigurationComponents = formConfigurationComponents;
        _formSelector = formSelector;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="formName">
    /// <see cref="IForm.Name"/>
    /// </param>
    /// <returns></returns>
    [NotNull]
    public IConfigurationComponent Get(string formName)
    {
        if (!_formConfigurationComponents.Any())
        {
            throw new AbpException("No form configuration component was registered! At least one component must be registered to be able to use the dynamic form components system.");
        }

        var form = _formSelector.Get(formName);
        var formConfigurationComponent = _formConfigurationComponents.FirstOrDefault(fp => fp.FormType == form.GetType());

        if (formConfigurationComponent == null)
            throw new AbpException(
                $"Could not find the form configuration component with the form type full name ({form.GetType().FullName}) ."
            );
        else
            return formConfigurationComponent;
    }
}