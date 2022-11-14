using System.Collections.Generic;
using System.Linq;
using Dignite.Abp.FieldCustomizing.Forms;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.FieldCustomizing.Components;

public class FieldFormComponentSelector : IFieldFormComponentSelector, ITransientDependency
{
    private readonly IEnumerable<IFieldFormComponent> _fieldControlComponents;
    private readonly IFormProviderSelector _fieldProviderSelector;

    public FieldFormComponentSelector(IEnumerable<IFieldFormComponent> fieldControlComponents, IFormProviderSelector fieldProviderSelector)
    {
        _fieldControlComponents = fieldControlComponents;
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
    public IFieldFormComponent Get(string formProviderName)
    {
        if (!_fieldControlComponents.Any())
        {
            throw new AbpException("No field control component was registered! At least one component must be registered to be able to use the field customizing system.");
        }

        var fieldProvider = _fieldProviderSelector.Get(formProviderName);
        var fieldControlComponent = _fieldControlComponents.FirstOrDefault(fp => fp.FormProviderType == fieldProvider.GetType());

        if (fieldControlComponent == null)
            throw new AbpException(
                $"Could not find the field control component with the field control provider type full name ({fieldProvider.GetType().FullName}) ."
            );
        else
            return fieldControlComponent;
    }
}