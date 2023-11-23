using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.DataDictionary;

public class DataDictionaryValueProviderManager : IDataDictionaryValueProviderManager, ISingletonDependency
{
    public List<IDataDictionaryValueProvider> Providers => _lazyProviders.Value;
    protected AbpDataDictionaryOptions Options { get; }
    private readonly Lazy<List<IDataDictionaryValueProvider>> _lazyProviders;

    public DataDictionaryValueProviderManager(
        IServiceProvider serviceProvider,
        IOptions<AbpDataDictionaryOptions> options)
    {

        Options = options.Value;

        _lazyProviders = new Lazy<List<IDataDictionaryValueProvider>>(
            () => Options
                .ValueProviders
                .Select(type => serviceProvider.GetRequiredService(type) as IDataDictionaryValueProvider)
                .ToList()!,
            true
        );
    }
}
