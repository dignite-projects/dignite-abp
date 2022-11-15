using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.DynamicForms.Components;

public class FormComponentSelector : IFormComponentSelector, ITransientDependency
{
    private readonly IEnumerable<IFormComponent> _formComponents;
    private readonly IFormProviderSelector _formProviderSelector;

    public FormComponentSelector(IEnumerable<IFormComponent> formComponents, IFormProviderSelector formProviderSelector)
    {
        _formComponents = formComponents;
        _formProviderSelector = formProviderSelector;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="formProviderName">
    /// <see cref="IFormProvider.Name"/>
    /// </param>
    /// <returns></returns>
    [NotNull]
    public IFormComponent Get(string formProviderName)
    {
        if (!_formComponents.Any())
        {
            throw new AbpException("No form component was registered! At least one component must be registered to be able to use the dynamic form components system.");
        }

        var formProvider = _formProviderSelector.Get(formProviderName);
        var formComponent = _formComponents.FirstOrDefault(fp => fp.FormProviderType == formProvider.GetType());

        if (formComponent == null)
            throw new AbpException(
                $"Could not find the form component with the form provider type full name ({formProvider.GetType().FullName}) ."
            );
        else
            return formComponent;
    }
}