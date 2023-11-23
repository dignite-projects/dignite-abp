using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dignite.Abp.DynamicForms;

namespace Dignite.Abp.DataDictionary;

public class GlobalDataDictionaryValueProvider : DataDictionaryValueProvider
{
    public const string ProviderName = "G";

    public override string Name => ProviderName;

    public GlobalDataDictionaryValueProvider(IDataDictionaryStore dataDictionariestore)
        : base(dataDictionariestore)
    {
    }

    public override Task<FormConfigurationDictionary> GetOrNullAsync(DataDictionaryDefinition dataDictionary)
    {
        return DataDictionaryStore.GetOrNullAsync(dataDictionary.Name, Name, null);
    }

    public override Task<List<DataDictionaryValue>> GetAllAsync(DataDictionaryDefinition[] dataDictionaries)
    {
        return DataDictionaryStore.GetAllAsync(dataDictionaries.Select(x => x.Name).ToArray(), Name, null);
    }
}
