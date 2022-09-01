using System;
using System.Collections.Generic;

namespace Dignite.Abp.FieldCustomizing
{
    [Serializable]
    public class CustomizeFieldDictionary : Dictionary<string, object>
    {
        public CustomizeFieldDictionary()
        {

        }

        public CustomizeFieldDictionary(IDictionary<string, object> dictionary)
            : base(dictionary)
        {
        }
    }
}
