using System;
using System.Collections.Generic;

namespace Dignite.Abp.DynamicForms;

/// <summary>
/// Dictionary for persisting dynamic form data
/// </summary>
/// <remarks>
/// Key:<see cref="ICustomizeFieldInfo.Name"/> name
/// Value: Dynamic form value
/// </remarks>
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