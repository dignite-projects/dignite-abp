using System;
using System.Collections.Generic;
using System.Linq;
using Dignite.Abp.DynamicForms.Switch;
using Dignite.Cms.Entries;
using JetBrains.Annotations;

namespace Dignite.Abp.Data;
public class SwitchFieldQuerying : FieldQueryingBase<SwitchFormControl>
{
    public SwitchFieldQuerying() : base()
    { }


    public override IEnumerable<Entry> Query([NotNull] IEnumerable<Entry> source, [NotNull] QueryingByField customField)
    {
        var value = bool.Parse(customField.Value);
        return source.Where(e => e.ExtraProperties.ContainsKey(customField.Name)
            && Convert.ToBoolean(e.ExtraProperties[customField.Name]) == value
        );
    }
}
