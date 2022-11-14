using System;
using System.Collections.Generic;

namespace Dignite.Abp.FieldCustomizing;

[Serializable]
public class CustomFieldDictionary : Dictionary<string, object>
{
    public CustomFieldDictionary()
    {
    }

    public CustomFieldDictionary(IDictionary<string, object> dictionary)
        : base(dictionary)
    {
    }
}