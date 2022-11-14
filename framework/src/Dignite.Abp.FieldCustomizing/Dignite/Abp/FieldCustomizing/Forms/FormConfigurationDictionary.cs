using System;
using System.Collections.Generic;

namespace Dignite.Abp.FieldCustomizing.Forms;

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