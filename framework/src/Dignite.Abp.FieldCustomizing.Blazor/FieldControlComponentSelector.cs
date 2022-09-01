using Dignite.Abp.FieldCustomizing.Fields;
using JetBrains.Annotations;
using System.Collections.Generic;
using System.Linq;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.FieldCustomizing.Blazor
{
    public class FieldControlComponentSelector : IFieldControlComponentSelector, ITransientDependency
    {
        private readonly IEnumerable<IFieldControlComponent> _fieldControlComponents;
        private readonly IFieldProviderSelector _fieldProviderSelector;

        public FieldControlComponentSelector(IEnumerable<IFieldControlComponent> fieldControlComponents, IFieldProviderSelector fieldProviderSelector)
        {
            _fieldControlComponents = fieldControlComponents;
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
        public IFieldControlComponent Get(string fieldProviderName)
        {
            if (!_fieldControlComponents.Any())
            {
                throw new AbpException("No field control component was registered! At least one component must be registered to be able to use the field customizing system.");
            }

            var fieldProvider = _fieldProviderSelector.Get(fieldProviderName);
            var fieldControlComponent = _fieldControlComponents.FirstOrDefault(fp => fp.FieldProviderType == fieldProvider.GetType());

            if (fieldControlComponent == null)
                throw new AbpException(
                    $"Could not find the field control component with the field control provider type full name ({fieldProvider.GetType().FullName}) ."
                );
            else
                return fieldControlComponent;
        }
    }
}
