using System;
using Dignite.Abp.DynamicForms;
using Volo.Abp;

namespace Dignite.Abp.DataDictionary;

[Serializable]
public class DataDictionaryValue : NameValue<FormConfigurationDictionary>
{
    public DataDictionaryValue()
    {

    }

    public DataDictionaryValue(string name, FormConfigurationDictionary value)
    {
        Name = name;
        Value = value;
    }
}
