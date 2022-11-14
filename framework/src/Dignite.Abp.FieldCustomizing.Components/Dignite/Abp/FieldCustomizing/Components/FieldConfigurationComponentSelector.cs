using System.Collections.Generic;
using System.Linq;
using Dignite.Abp.FieldCustomizing.Forms;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.FieldCustomizing.Components;

public class FieldConfigurationComponentSelector : IFieldConfigurationComponentSelector, ITransientDependency
{
    private readonly IEnumerable<IFieldConfigurationComponent> _fieldConfigurationComponents;
    private readonly IFormProviderSelector _fieldProviderSelector;

    public FieldConfigurationComponentSelector(IEnumerable<IFieldConfigurationComponent> fieldConfigurationComponents, IFormProviderSelector fieldProviderSelector)
    {
        _fieldConfigurationComponents = fieldConfigurationComponents;
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
    public IFieldConfigurationComponent Get(string formProviderName)
    {
        if (!_fieldConfigurationComponents.Any())
        {
            throw new AbpException("No field control component was registered! At least one component must be registered to be able to use the field customizing system.");
        }

        var fieldProvider = _fieldProviderSelector.Get(formProviderName);
        var fieldConfigurationComponent = _fieldConfigurationComponents.FirstOrDefault(fp => fp.FormProviderType == fieldProvider.GetType());

        if (fieldConfigurationComponent == null)
            throw new AbpException(
                $"Could not find the field control component with the field control provider type full name ({fieldProvider.GetType().FullName}) ."
            );
        else
            return fieldConfigurationComponent;
    }
}