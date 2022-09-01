using System;
using System.Collections.Generic;

namespace Dignite.Abp.FieldCustomizing.Fields
{
    [Serializable]
    public class FieldConfigurationDictionary : Dictionary<string, string>
    {

        public FieldConfigurationDictionary()
        {

        }

        public FieldConfigurationDictionary(IDictionary<string, string> dictionary)
            : base(dictionary)
        {
        }
    }
}

