using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dignite.Abp.DynamicForms;

namespace Dignite.Abp.DataDictionary;

public class DefaultValueDataDictionaryValueProvider : DataDictionaryValueProvider
{
    public const string ProviderName = "D";

    public override string Name => ProviderName;

    public DefaultValueDataDictionaryValueProvider(IDataDictionaryStore dataDictionariestore)
        : base(dataDictionariestore)
    {

    }

    public override Task<FormConfigurationDictionary> GetOrNullAsync(DataDictionaryDefinition dataDictionary)
    {
        return Task.FromResult(dataDictionary.DefaultValue);
    }

    public override Task<List<DataDictionaryValue>> GetAllAsync(DataDictionaryDefinition[] dataDictionaries)
    {
        return Task.FromResult(dataDictionaries.Select(x => new DataDictionaryValue(x.Name, x.DefaultValue)).ToList());
    }
}
