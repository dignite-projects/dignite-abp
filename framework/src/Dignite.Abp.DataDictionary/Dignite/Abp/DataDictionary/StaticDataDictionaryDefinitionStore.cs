using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.DataDictionary;

public class StaticDataDictionaryDefinitionStore : IStaticDataDictionaryDefinitionStore, ISingletonDependency
{
    protected Lazy<IDictionary<string, DataDictionaryDefinition>> DataDictionaryDefinitions { get; }

    protected AbpDataDictionaryOptions Options { get; }

    protected IServiceProvider ServiceProvider { get; }

    public StaticDataDictionaryDefinitionStore(IOptions<AbpDataDictionaryOptions> options, IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
        Options = options.Value;

        DataDictionaryDefinitions = new Lazy<IDictionary<string, DataDictionaryDefinition>>(CreateDataDictionaryDefinitions, true);
    }

    public virtual async Task<DataDictionaryDefinition> GetAsync(string name)
    {
        Check.NotNull(name, nameof(name));

        var dataDictionary = await GetOrNullAsync(name);

        if (dataDictionary == null)
        {
            throw new AbpException("Undefined dataDictionary: " + name);
        }

        return dataDictionary;
    }

    public virtual Task<IReadOnlyList<DataDictionaryDefinition>> GetAllAsync()
    {
        return Task.FromResult<IReadOnlyList<DataDictionaryDefinition>>(DataDictionaryDefinitions.Value.Values.ToImmutableList());
    }

    public virtual Task<DataDictionaryDefinition?> GetOrNullAsync(string name)
    {
        return Task.FromResult(DataDictionaryDefinitions.Value.GetOrDefault(name));
    }

    protected virtual IDictionary<string, DataDictionaryDefinition> CreateDataDictionaryDefinitions()
    {
        var dataDictionaries = new Dictionary<string, DataDictionaryDefinition>();

        using (var scope = ServiceProvider.CreateScope())
        {
            var providers = Options
                .DefinitionProviders
                .Select(p => scope.ServiceProvider.GetRequiredService(p) as IDataDictionaryDefinitionProvider)
                .ToList();

            foreach (var provider in providers)
            {
                provider?.Define(new DataDictionaryDefinitionContext(dataDictionaries));
            }
        }

        return dataDictionaries;
    }
}
