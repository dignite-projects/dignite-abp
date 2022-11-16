using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.DynamicForms.Components;

public class FieldComponentSelector : IFieldComponentSelector, ITransientDependency
{
    private readonly IEnumerable<IFieldComponent> _fieldComponents;
    private readonly IFormSelector _formSelector;

    public FieldComponentSelector(IEnumerable<IFieldComponent> fieldComponents, IFormSelector formSelector)
    {
        _fieldComponents = fieldComponents;
        _formSelector = formSelector;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [NotNull]
    public IFieldComponent Get(string formName)
    {
        if (!_fieldComponents.Any())
        {
            throw new AbpException("No field component was registered! At least one component must be registered to be able to use the dynamic form components system.");
        }

        var form = _formSelector.Get(formName);
        var fieldComponent = _fieldComponents.LastOrDefault(fp => fp.FormType == form.GetType());

        if (fieldComponent == null)
            throw new AbpException(
                $"Could not find the field component with the form type full name ({form.GetType().FullName}) ."
            );
        else
            return fieldComponent;
    }
}