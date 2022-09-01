using Dignite.Abp.FieldCustomizing.Fields;
using JetBrains.Annotations;

namespace Dignite.Abp.FieldCustomizing.Blazor
{
    public interface IFieldConfigurationComponentSelector
    {
        /// <summary>
        /// Get blazor component using field control provider name
        /// </summary>
        /// <param name="fieldProviderName">
        /// <see cref="IFieldProvider.Name"/>
        /// </param>
        /// <returns></returns>
        [NotNull]
        IFieldConfigurationComponent Get(string fieldProviderName);
    }
}
