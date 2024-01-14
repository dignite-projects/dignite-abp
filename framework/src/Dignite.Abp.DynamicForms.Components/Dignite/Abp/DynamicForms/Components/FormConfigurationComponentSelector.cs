using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.DynamicForms.Components;

public class FormConfigurationComponentSelector : IFormConfigurationComponentSelector, ITransientDependency
{
    private readonly IEnumerable<IFormConfigurationComponent> _formConfigurationComponents;
    private readonly IFormControlSelector _formControlSelector;

    public FormConfigurationComponentSelector(IEnumerable<IFormConfigurationComponent> formConfigurationComponents, IFormControlSelector formControlSelector)
    {
        _formConfigurationComponents = formConfigurationComponents;
        _formControlSelector = formControlSelector;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="formControlName">
    /// <see cref="IFormControl.Name"/>
    /// </param>
    /// <returns></returns>
    [NotNull]
    public IFormConfigurationComponent Get(string formControlName)
    {
        if (!_formConfigurationComponents.Any())
        {
            throw new AbpException("No form configuration component was registered! At least one component must be registered to be able to use the dynamic form components system.");
        }

        var formControl = _formControlSelector.Get(formControlName);
        var formConfigurationComponent = _formConfigurationComponents.FirstOrDefault(fp => fp.FormControlType == formControl.GetType());

        if (formConfigurationComponent == null)
            throw new AbpException(
                $"Could not find the form configuration component with the form control type full name ({formControl.GetType().FullName}) ."
            );
        else
            return formConfigurationComponent;
    }
}