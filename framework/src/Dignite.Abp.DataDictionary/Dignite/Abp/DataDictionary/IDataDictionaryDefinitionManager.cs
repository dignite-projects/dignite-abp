using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Dignite.Abp.DataDictionary;

public interface IDataDictionaryDefinitionManager
{
    [ItemNotNull]
    Task<DataDictionaryDefinition> GetAsync([NotNull] string name);

    [ItemNotNull]
    Task<IReadOnlyList<DataDictionaryDefinition>> GetAllAsync();

    [ItemCanBeNull]
    Task<DataDictionaryDefinition> GetOrNullAsync([NotNull] string name);
}
