using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.DynamicForms.Components;

public class FormViewComponentSelector : IFormViewComponentSelector, ITransientDependency
{
    private readonly IEnumerable<IFormViewComponent> _fieldComponents;
    private readonly IFormControlSelector _formControlSelector;

    public FormViewComponentSelector(IEnumerable<IFormViewComponent> fieldComponents, IFormControlSelector formControlSelector)
    {
        _fieldComponents = fieldComponents;
        _formControlSelector = formControlSelector;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [NotNull]
    public IFormViewComponent Get(string formControlName)
    {
        if (!_fieldComponents.Any())
        {
            throw new AbpException("No field component was registered! At least one component must be registered to be able to use the dynamic form components system.");
        }

        var form = _formControlSelector.Get(formControlName);
        var fieldComponent = _fieldComponents.LastOrDefault(fp => fp.FormControlType == form.GetType());

        if (fieldComponent == null)
            throw new AbpException(
                $"Could not find the field component with the form type full name ({form.GetType().FullName}) ."
            );
        else
            return fieldComponent;
    }
}