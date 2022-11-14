using System.Collections.Generic;
using System.Linq;
using Dignite.Abp.FieldCustomizing.Forms;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.FieldCustomizing.Components;

public class FieldComponentSelector : IFieldComponentSelector, ITransientDependency
{
    private readonly IEnumerable<IFieldComponent> _fieldComponents;
    private readonly IFormProviderSelector _fieldProviderSelector;

    public FieldComponentSelector(IEnumerable<IFieldComponent> fieldComponents, IFormProviderSelector fieldProviderSelector)
    {
        _fieldComponents = fieldComponents;
        _fieldProviderSelector = fieldProviderSelector;
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
            throw new AbpException("No field component was registered! At least one component must be registered to be able to use the field customizing system.");
        }

        var fieldProvider = _fieldProviderSelector.Get(formProviderName);
        var fieldComponent = _fieldComponents.LastOrDefault(fp => fp.FormProviderType == fieldProvider.GetType());

        if (fieldComponent == null)
            throw new AbpException(
                $"Could not find the field component with the field provider type full name ({fieldProvider.GetType().FullName}) ."
            );
        else
            return fieldComponent;
    }
}