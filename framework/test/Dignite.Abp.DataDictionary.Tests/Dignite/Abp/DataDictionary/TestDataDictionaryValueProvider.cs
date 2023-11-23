using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dignite.Abp.DynamicForms;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.DataDictionary;

public class TestDataDictionaryValueProvider : IDataDictionaryValueProvider, ITransientDependency
{
    public const string ProviderName = "Test";

    private readonly Dictionary<string, FormConfigurationDictionary> _values;

    public string Name => ProviderName;

    public TestDataDictionaryValueProvider()
    {
        _values = new Dictionary<string, FormConfigurationDictionary>();
    }

    public Task<FormConfigurationDictionary> GetOrNullAsync(DataDictionaryDefinition setting)
    {
        return Task.FromResult(_values.GetOrDefault(setting.Name));
    }

    public Task<List<DataDictionaryValue>> GetAllAsync(DataDictionaryDefinition[] settings)
    {
        return Task.FromResult(settings.Select(x => new DataDictionaryValue(x.Name, _values.GetOrDefault(x.Name))).ToList());
    }
}
