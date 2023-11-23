using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Dignite.Abp.DataDictionary;

public interface IDynamicDataDictionaryDefinitionStore
{
    Task<DataDictionaryDefinition> GetAsync([NotNull] string name);

    Task<IReadOnlyList<DataDictionaryDefinition>> GetAllAsync();

    Task<DataDictionaryDefinition?> GetOrNullAsync([NotNull] string name);
}
