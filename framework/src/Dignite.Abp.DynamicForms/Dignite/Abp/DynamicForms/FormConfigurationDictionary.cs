using System;
using System.Collections.Generic;

namespace Dignite.Abp.DynamicForms;

/// <summary>
/// Configuration Item Dictionary for Forms
/// </summary>
[Serializable]
public class FormConfigurationDictionary : Dictionary<string, object>
{
    public FormConfigurationDictionary()
    {
    }

    public FormConfigurationDictionary(IDictionary<string, object> dictionary)
        : base(dictionary)
    {
    }
}