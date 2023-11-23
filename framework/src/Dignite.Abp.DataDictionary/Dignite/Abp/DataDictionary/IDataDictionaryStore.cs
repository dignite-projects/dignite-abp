using System.Collections.Generic;
using System.Threading.Tasks;
using Dignite.Abp.DynamicForms;
using JetBrains.Annotations;

namespace Dignite.Abp.DataDictionary;

public interface IDataDictionaryStore
{
    Task<FormConfigurationDictionary> GetOrNullAsync(
        [NotNull] string name,
        string? providerName,
        string? providerKey
    );

    Task<List<DataDictionaryValue>> GetAllAsync(
        [NotNull] string[] names,
        string? providerName,
        string? providerKey
    );
}
