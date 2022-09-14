using System;
using System.Linq;
using Dignite.Abp.FieldCustomizing.Fields;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Dignite.Abp.FieldCustomizing.EntityFrameworkCore.ValueComparers
{
    public class CustomizedFieldConfigurationDictionaryValueComparer : ValueComparer<FieldConfigurationDictionary>
    {
        public CustomizedFieldConfigurationDictionaryValueComparer()
            : base(
                  (d1, d2) => d1.SequenceEqual(d2),
                  d => d.Aggregate(0, (k, v) => HashCode.Combine(k, v.GetHashCode())),
                  d => new FieldConfigurationDictionary(d))
        {
        }
    }
}