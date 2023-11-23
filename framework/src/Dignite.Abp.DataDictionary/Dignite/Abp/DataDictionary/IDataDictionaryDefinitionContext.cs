using System.Collections.Generic;

namespace Dignite.Abp.DataDictionary;

public interface IDataDictionaryDefinitionContext
{
    DataDictionaryDefinition? GetOrNull(string name);

    IReadOnlyList<DataDictionaryDefinition> GetAll();

    void Add(params DataDictionaryDefinition[] definitions);
}
