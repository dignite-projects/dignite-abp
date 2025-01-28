using Dignite.Abp.DynamicForms.Select;
using JetBrains.Annotations;
using System.Collections.Generic;
using System.Linq;
using Dignite.Cms.Entries;

namespace Dignite.Abp.Data;
public class SelectFieldQuerying : FieldQueryingBase<SelectFormControl>
{

    public SelectFieldQuerying() : base()
    {
    }


    public override IEnumerable<Entry> Query([NotNull] IEnumerable<Entry> source, [NotNull] QueryingByField customField)
    {
        var value = customField.Value.Split(',');
        return source.Where(e => e.ExtraProperties.ContainsKey(customField.Name) &&
            e.GetField<string[]>(customField.Name).Any(v => value.Contains(v))
        );
    }
}
