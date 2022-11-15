using System;
using System.Collections.Generic;

namespace Dignite.Abp.DynamicForms;

/// <summary>
/// Configuration Item Dictionary for Dynamic Forms
/// </summary>
[Serializable]
public class FormConfigurationDictionary : Dictionary<string, string>
{
    public FormConfigurationDictionary()
    {
    }

    public FormConfigurationDictionary(IDictionary<string, string> dictionary)
        : base(dictionary)
    {
    }
}