using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.DynamicForms.Components;

public class FormComponentSelector : IFormComponentSelector, ITransientDependency
{
    private readonly IEnumerable<IFormComponent> _formComponents;
    private readonly IFormSelector _formSelector;

    public FormComponentSelector(IEnumerable<IFormComponent> formComponents, IFormSelector formSelector)
    {
        _formComponents = formComponents;
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
    public IFormComponent Get(string formName)
    {
        if (!_formComponents.Any())
        {
            throw new AbpException("No form component was registered! At least one component must be registered to be able to use the dynamic form components system.");
        }

        var form = _formSelector.Get(formName);
        var formComponent = _formComponents.FirstOrDefault(fp => fp.FormType == form.GetType());

        if (formComponent == null)
            throw new AbpException(
                $"Could not find the form component with the form type full name ({form.GetType().FullName}) ."
            );
        else
            return formComponent;
    }
}