using Dignite.Abp.FieldCustomizing.Fields;
using JetBrains.Annotations;
using System.Collections.Generic;
using System.Linq;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.FieldCustomizing.Blazor
{
    public class FieldConfigurationComponentSelector : IFieldConfigurationComponentSelector, ITransientDependency
    {
        private readonly IEnumerable<IFieldConfigurationComponent> _fieldConfigurationComponents;
        private readonly IFieldProviderSelector _fieldProviderSelector;

        public FieldConfigurationComponentSelector(IEnumerable<IFieldConfigurationComponent> fieldConfigurationComponents, IFieldProviderSelector fieldProviderSelector)
        {
            _fieldConfigurationComponents = fieldConfigurationComponents;
            _fieldProviderSelector = fieldProviderSelector;
        }


        /// <summary>
        /// Get blazor component using field control provider name
        /// </summary>
        /// <param name="fieldProviderName">
        /// <see cref="IFieldProvider.Name"/>
        /// </param>
        /// <returns></returns>
        [NotNull]
        public IFieldConfigurationComponent Get(string fieldProviderName)
        {
            if (!_fieldConfigurationComponents.Any())
            {
                throw new AbpException("No field control component was registered! At least one component must be registered to be able to use the field customizing system.");
            }

            var fieldProvider = _fieldProviderSelector.Get(fieldProviderName);
            var fieldConfigurationComponent = _fieldConfigurationComponents.FirstOrDefault(fp => fp.FieldProviderType == fieldProvider.GetType());

            if (fieldConfigurationComponent == null)
                throw new AbpException(
                    $"Could not find the field control component with the field control provider type full name ({fieldProvider.GetType().FullName}) ."
                );
            else
                return fieldConfigurationComponent;
        }
    }
}
