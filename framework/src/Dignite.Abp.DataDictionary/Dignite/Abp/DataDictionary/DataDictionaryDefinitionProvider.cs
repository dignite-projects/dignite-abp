using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.DataDictionary;

public abstract class DataDictionaryDefinitionProvider : IDataDictionaryDefinitionProvider, ITransientDependency
{
    public abstract void Define(IDataDictionaryDefinitionContext context);
}
