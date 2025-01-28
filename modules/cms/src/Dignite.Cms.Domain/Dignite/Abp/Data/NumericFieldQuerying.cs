using System;
using System.Collections.Generic;
using System.Linq;
using Dignite.Abp.DynamicForms.NumericEdit;
using Dignite.Cms.Entries;
using JetBrains.Annotations;

namespace Dignite.Abp.Data;
public class NumericFieldQuerying : FieldQueryingBase<NumericEditFormControl>
{
    public NumericFieldQuerying() : base()
    { }


    public override IEnumerable<Entry> Query([NotNull] IEnumerable<Entry> source, [NotNull] QueryingByField customField)
    {
        var min = double.Parse(customField.Value.Split('-')[0]);
        var max = double.Parse(customField.Value.Split('-')[1]);
        return source.Where(e => e.ExtraProperties.ContainsKey(customField.Name)
            && Convert.ToDouble(e.ExtraProperties[customField.Name]) >= min
            && Convert.ToDouble(e.ExtraProperties[customField.Name]) < max
        );
    }
}
