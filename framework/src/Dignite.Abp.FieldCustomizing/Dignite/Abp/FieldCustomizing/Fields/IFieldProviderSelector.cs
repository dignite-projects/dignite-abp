using JetBrains.Annotations;

namespace Dignite.Abp.FieldCustomizing.Fields;

public interface IFieldProviderSelector
{
    /// <summary>
    /// Get provider using field name
    /// </summary>
    /// <param name="providerName">
    /// <see cref="IFieldProvider.Name"/>
    /// </param>
    /// <returns></returns>
    [NotNull]
    IFieldProvider Get(string providerName);
}