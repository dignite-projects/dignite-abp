using System.Collections.Generic;
using System.Collections.Immutable;

namespace Dignite.Abp.DataDictionary;

public class DataDictionaryDefinitionContext : IDataDictionaryDefinitionContext
{
    protected Dictionary<string, DataDictionaryDefinition> DataDictionaries { get; }

    public DataDictionaryDefinitionContext(Dictionary<string, DataDictionaryDefinition> dataDictionaries)
    {
        DataDictionaries = dataDictionaries;
    }

    public virtual DataDictionaryDefinition? GetOrNull(string name)
    {
        return DataDictionaries.GetOrDefault(name);
    }

    public virtual IReadOnlyList<DataDictionaryDefinition> GetAll()
    {
        return DataDictionaries.Values.ToImmutableList();
    }

    public virtual void Add(params DataDictionaryDefinition[] definitions)
    {
        if (definitions.IsNullOrEmpty())
        {
            return;
        }

        foreach (var definition in definitions)
        {
            DataDictionaries[definition.Name] = definition;
        }
    }
}
