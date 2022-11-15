using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.DynamicForms.Components;

public class ConfigurationComponentSelector : IConfigurationComponentSelector, ITransientDependency
{
    private readonly IEnumerable<IConfigurationComponent> _formConfigurationComponents;
    private readonly IFormProviderSelector _formProviderSelector;

    public ConfigurationComponentSelector(IEnumerable<IConfigurationComponent> formConfigurationComponents, IFormProviderSelector formProviderSelector)
    {
        _formConfigurationComponents = formConfigurationComponents;
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
    public IConfigurationComponent Get(string formProviderName)
    {
        if (!_formConfigurationComponents.Any())
        {
            throw new AbpException("No form configuration component was registered! At least one component must be registered to be able to use the dynamic form components system.");
        }

        var formProvider = _formProviderSelector.Get(formProviderName);
        var formConfigurationComponent = _formConfigurationComponents.FirstOrDefault(fp => fp.FormProviderType == formProvider.GetType());

        if (formConfigurationComponent == null)
            throw new AbpException(
                $"Could not find the form configuration component with the form configuration provider type full name ({formProvider.GetType().FullName}) ."
            );
        else
            return formConfigurationComponent;
    }
}