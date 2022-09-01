using Dignite.Abp.FieldCustomizing.Fields;
using JetBrains.Annotations;
using System.Collections.Generic;
using System.Linq;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.FieldCustomizing.Blazor
{
    public class FieldComponentSelector : IFieldComponentSelector, ITransientDependency
    {
        private readonly IEnumerable<IFieldComponent> _fieldComponents;
        private readonly IFieldProviderSelector _fieldProviderSelector;

        public FieldComponentSelector(IEnumerable<IFieldComponent> fieldComponents, IFieldProviderSelector fieldProviderSelector)
        {
            _fieldComponents = fieldComponents;
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
        public IFieldComponent Get(string fieldProviderName)
        {
            if (!_fieldComponents.Any())
            {
                throw new AbpException("No field component was registered! At least one component must be registered to be able to use the field customizing system.");
            }

            var fieldProvider = _fieldProviderSelector.Get(fieldProviderName);
            var fieldComponent = _fieldComponents.FirstOrDefault(fp => fp.FieldProviderType == fieldProvider.GetType());

            if (fieldComponent == null)
                throw new AbpException(
                    $"Could not find the field component with the field provider type full name ({fieldProvider.GetType().FullName}) ."
                );
            else
                return fieldComponent;
        }
    }
}
