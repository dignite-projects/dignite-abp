using System.Collections.Generic;
using System.Threading.Tasks;
using Dignite.Abp.DynamicForms;
using JetBrains.Annotations;

namespace Dignite.Abp.DataDictionary;

public interface IDataDictionaryValueProvider
{
    string Name { get; }

    Task<FormConfigurationDictionary> GetOrNullAsync([NotNull] DataDictionaryDefinition dataDictionary);

    Task<List<DataDictionaryValue>> GetAllAsync([NotNull] DataDictionaryDefinition[] dataDictionaries);
}
