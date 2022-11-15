using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.DynamicForms.Components;

public class FieldComponentSelector : IFieldComponentSelector, ITransientDependency
{
    private readonly IEnumerable<IFieldComponent> _fieldComponents;
    private readonly IFormProviderSelector _formProviderSelector;

    public FieldComponentSelector(IEnumerable<IFieldComponent> fieldComponents, IFormProviderSelector formProviderSelector)
    {
        _fieldComponents = fieldComponents;
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
    public IFieldComponent Get(string formProviderName)
    {
        if (!_fieldComponents.Any())
        {
            throw new AbpException("No field component was registered! At least one component must be registered to be able to use the dynamic form components system.");
        }

        var formProvider = _formProviderSelector.Get(formProviderName);
        var fieldComponent = _fieldComponents.LastOrDefault(fp => fp.FormProviderType == formProvider.GetType());

        if (fieldComponent == null)
            throw new AbpException(
                $"Could not find the field component with the field provider type full name ({formProvider.GetType().FullName}) ."
            );
        else
            return fieldComponent;
    }
}