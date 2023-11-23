using System.Collections.Generic;

namespace Dignite.Abp.DataDictionary;

public interface IDataDictionaryValueProviderManager
{
    List<IDataDictionaryValueProvider> Providers { get; }
}
