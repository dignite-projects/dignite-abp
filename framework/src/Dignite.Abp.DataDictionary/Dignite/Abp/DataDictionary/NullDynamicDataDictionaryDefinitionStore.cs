using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.DataDictionary;

public class NullDynamicDataDictionaryDefinitionStore : IDynamicDataDictionaryDefinitionStore, ISingletonDependency
{
    private readonly static Task<DataDictionaryDefinition?> CachedNullableDataDictionaryResult = Task.FromResult((DataDictionaryDefinition?)null);
    private readonly static Task<DataDictionaryDefinition> CachedDataDictionaryResult = Task.FromResult((DataDictionaryDefinition)null!);

    private readonly static Task<IReadOnlyList<DataDictionaryDefinition>> CachedDataDictionariesResult = Task.FromResult((IReadOnlyList<DataDictionaryDefinition>)Array.Empty<DataDictionaryDefinition>().ToImmutableList());

    public Task<DataDictionaryDefinition> GetAsync(string name)
    {
        return CachedDataDictionaryResult;
    }

    public Task<IReadOnlyList<DataDictionaryDefinition>> GetAllAsync()
    {
        return CachedDataDictionariesResult;
    }

    public Task<DataDictionaryDefinition?> GetOrNullAsync(string name)
    {
        return CachedNullableDataDictionaryResult;
    }
}
