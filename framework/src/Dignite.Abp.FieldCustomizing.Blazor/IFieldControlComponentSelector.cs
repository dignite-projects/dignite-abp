using Dignite.Abp.FieldCustomizing.Fields;
using JetBrains.Annotations;

namespace Dignite.Abp.FieldCustomizing.Blazor
{
    public interface IFieldControlComponentSelector
    {
        /// <summary>
        /// Get blazor component using field control provider name
        /// </summary>
        /// <param name="fieldProviderName">
        /// <see cref="IFieldProvider.Name"/>
        /// </param>
        /// <returns></returns>
        [NotNull]
        IFieldControlComponent Get(string fieldProviderName);
    }
}
