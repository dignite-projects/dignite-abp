using Volo.Abp.Collections;

namespace Dignite.Abp.DataDictionary;

public class AbpDataDictionaryOptions
{
    public ITypeList<IDataDictionaryDefinitionProvider> DefinitionProviders { get; }

    public ITypeList<IDataDictionaryValueProvider> ValueProviders { get; }

    public AbpDataDictionaryOptions()
    {
        DefinitionProviders = new TypeList<IDataDictionaryDefinitionProvider>();
        ValueProviders = new TypeList<IDataDictionaryValueProvider>();
    }
}
