using System.Collections.Generic;
using System.Threading.Tasks;
using Dignite.Abp.DynamicForms;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.DataDictionary;

public abstract class DataDictionaryValueProvider : IDataDictionaryValueProvider, ITransientDependency
{
    public abstract string Name { get; }

    protected IDataDictionaryStore DataDictionaryStore { get; }

    protected DataDictionaryValueProvider(IDataDictionaryStore dataDictionaryStore)
    {
        DataDictionaryStore = dataDictionaryStore;
    }

    public abstract Task<FormConfigurationDictionary> GetOrNullAsync(DataDictionaryDefinition dataDictionary);

    public abstract Task<List<DataDictionaryValue>> GetAllAsync(DataDictionaryDefinition[] dataDictionaries);
}
