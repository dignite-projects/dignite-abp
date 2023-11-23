using System.Collections.Generic;
using System.Threading.Tasks;
using Dignite.Abp.DynamicForms;
using JetBrains.Annotations;

namespace Dignite.Abp.DataDictionary;

public interface IDataDictionaryProvider
{
    Task<FormConfigurationDictionary> GetOrNullAsync([NotNull] string name);

    Task<List<DataDictionaryValue>> GetAllAsync([NotNull] string[] names);

    Task<List<DataDictionaryValue>> GetAllAsync();
}
