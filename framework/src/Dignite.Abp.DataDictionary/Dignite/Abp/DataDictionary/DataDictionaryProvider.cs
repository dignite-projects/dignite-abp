using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dignite.Abp.DynamicForms;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.DataDictionary;

public class DataDictionaryProvider : IDataDictionaryProvider, ITransientDependency
{
    protected IDataDictionaryDefinitionManager DataDictionaryDefinitionManager { get; }
    protected IDataDictionaryValueProviderManager DataDictionaryValueProviderManager { get; }

    public DataDictionaryProvider(
        IDataDictionaryDefinitionManager dataDictionaryDefinitionManager,
        IDataDictionaryValueProviderManager dataDictionaryValueProviderManager)
    {
        DataDictionaryDefinitionManager = dataDictionaryDefinitionManager;
        DataDictionaryValueProviderManager = dataDictionaryValueProviderManager;
    }

    public virtual async Task<FormConfigurationDictionary> GetOrNullAsync(string name)
    {
        var dataDictionary = await DataDictionaryDefinitionManager.GetAsync(name);
        var providers = Enumerable
            .Reverse(DataDictionaryValueProviderManager.Providers);

        if (dataDictionary.Providers.Any())
        {
            providers = providers.Where(p => dataDictionary.Providers.Contains(p.Name));
        }

        var value = await GetOrNullValueFromProvidersAsync(providers, dataDictionary);

        return value;
    }

    public virtual async Task<List<DataDictionaryValue>> GetAllAsync(string[] names)
    {
        var result = new Dictionary<string, DataDictionaryValue>();
        var dataDictionaryDefinitions = (await DataDictionaryDefinitionManager.GetAllAsync()).Where(x => names.Contains(x.Name)).ToList();

        foreach (var definition in dataDictionaryDefinitions)
        {
            result.Add(definition.Name, new DataDictionaryValue(definition.Name, null));
        }

        foreach (var provider in Enumerable.Reverse(DataDictionaryValueProviderManager.Providers))
        {
            var dataDictionaryValues = await provider.GetAllAsync(dataDictionaryDefinitions.Where(x => !x.Providers.Any() || x.Providers.Contains(provider.Name)).ToArray());

            var notNullValues = dataDictionaryValues.Where(x => x.Value != null).ToList();
            foreach (var dataDictionaryValue in notNullValues)
            {
                var dataDictionaryDefinition = dataDictionaryDefinitions.First(x => x.Name == dataDictionaryValue.Name);

                if (result.ContainsKey(dataDictionaryValue.Name) && result[dataDictionaryValue.Name].Value == null)
                {
                    result[dataDictionaryValue.Name].Value = dataDictionaryValue.Value;
                }
            }

            dataDictionaryDefinitions.RemoveAll(x => notNullValues.Any(v => v.Name == x.Name));
            if (!dataDictionaryDefinitions.Any())
            {
                break;
            }
        }

        return result.Values.ToList();
    }

    public virtual async Task<List<DataDictionaryValue>> GetAllAsync()
    {
        var dataDictionaryValues = new List<DataDictionaryValue>();
        var dataDictionaryDefinitions = await DataDictionaryDefinitionManager.GetAllAsync();

        foreach (var dataDictionary in dataDictionaryDefinitions)
        {
            dataDictionaryValues.Add(new DataDictionaryValue(dataDictionary.Name, await GetOrNullAsync(dataDictionary.Name)));
        }

        return dataDictionaryValues;
    }

    protected virtual async Task<FormConfigurationDictionary> GetOrNullValueFromProvidersAsync(
        IEnumerable<IDataDictionaryValueProvider> providers,
        DataDictionaryDefinition dataDictionary)
    {
        foreach (var provider in providers)
        {
            var value = await provider.GetOrNullAsync(dataDictionary);
            if (value != null)
            {
                return value;
            }
        }

        return null;
    }
}
