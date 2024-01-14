using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.DynamicForms.Components;

public class FormControlComponentSelector : IFormControlComponentSelector, ITransientDependency
{
    private readonly IEnumerable<IFormControlComponent> _formComponents;
    private readonly IFormControlSelector _formControlSelector;

    public FormControlComponentSelector(IEnumerable<IFormControlComponent> formComponents, IFormControlSelector formControlSelector)
    {
        _formComponents = formComponents;
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
    public IFormControlComponent Get(string formControlName)
    {
        if (!_formComponents.Any())
        {
            throw new AbpException("No form component was registered! At least one component must be registered to be able to use the dynamic form components system.");
        }

        var form = _formControlSelector.Get(formControlName);
        var formComponent = _formComponents.FirstOrDefault(fp => fp.FormControlType == form.GetType());

        if (formComponent == null)
            throw new AbpException(
                $"Could not find the form component with the form type full name ({form.GetType().FullName}) ."
            );
        else
            return formComponent;
    }
}