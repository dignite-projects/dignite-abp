using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dignite.Abp.DynamicForms;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.DataDictionary;

[Dependency(TryRegister = true)]
public class NullDataDictionaryStore : IDataDictionaryStore, ISingletonDependency
{
    public ILogger<NullDataDictionaryStore> Logger { get; set; }

    public NullDataDictionaryStore()
    {
        Logger = NullLogger<NullDataDictionaryStore>.Instance;
    }

    public Task<FormConfigurationDictionary> GetOrNullAsync(string name, string? providerName, string? providerKey)
    {
        return Task.FromResult((FormConfigurationDictionary)null);
    }

    public Task<List<DataDictionaryValue>> GetAllAsync(string[] names, string? providerName, string? providerKey)
    {
        return Task.FromResult(names.Select(x => new DataDictionaryValue(x, null)).ToList());
    }
}
