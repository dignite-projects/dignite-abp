using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.DataDictionary;

public class DataDictionaryDefinitionManager : IDataDictionaryDefinitionManager, ISingletonDependency
{
    protected readonly IStaticDataDictionaryDefinitionStore StaticStore;
    protected readonly IDynamicDataDictionaryDefinitionStore DynamicStore;

    public DataDictionaryDefinitionManager(IStaticDataDictionaryDefinitionStore staticStore, IDynamicDataDictionaryDefinitionStore dynamicStore)
    {
        StaticStore = staticStore;
        DynamicStore = dynamicStore;
    }

    public virtual async Task<DataDictionaryDefinition> GetAsync(string name)
    {
        var dataDictionary = await GetOrNullAsync(name);
        if (dataDictionary == null)
        {
            throw new AbpException("Undefined dataDictionary: " + name);
        }

        return dataDictionary;
    }

    public virtual async Task<DataDictionaryDefinition?> GetOrNullAsync(string name)
    {
        Check.NotNull(name, nameof(name));

        return await StaticStore.GetOrNullAsync(name) ?? await DynamicStore.GetOrNullAsync(name);
    }

    public virtual async Task<IReadOnlyList<DataDictionaryDefinition>> GetAllAsync()
    {
        var staticDataDictionaries = await StaticStore.GetAllAsync();
        var staticDataDictionaryNames = staticDataDictionaries
            .Select(p => p.Name)
            .ToImmutableHashSet();

        var dynamicDataDictionaries = await DynamicStore.GetAllAsync();

        /* We prefer static dataDictionaries over dynamics */
        return staticDataDictionaries.Concat(dynamicDataDictionaries.Where(d => !staticDataDictionaryNames.Contains(d.Name))).ToImmutableList();
    }
}
