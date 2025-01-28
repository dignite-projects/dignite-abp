using Dignite.Abp.DynamicForms.Entry;
using Dignite.Cms.Entries;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dignite.Abp.Data;
public class EntryFieldQuerying : FieldQueryingBase<EntryFormControl>
{

    public EntryFieldQuerying() : base()
    {
    }


    public override IEnumerable<Entry> Query([NotNull] IEnumerable<Entry> source, [NotNull] QueryingByField customField)
    {
        var values = customField.Value.Split(',').Select(v=>Guid.Parse(v));
        return source.Where(e => e.ExtraProperties.ContainsKey(customField.Name) &&
            e.GetField<Guid[]>(customField.Name).Any(v => values.Contains(v))
        );
    }
}
