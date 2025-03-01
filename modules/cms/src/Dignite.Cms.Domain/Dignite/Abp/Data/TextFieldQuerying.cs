using System.Collections.Generic;
using System.Linq;
using Dignite.Abp.DynamicForms.TextEdit;
using Dignite.Cms.Entries;
using JetBrains.Annotations;

namespace Dignite.Abp.Data;
public class TextFieldQuerying : FieldQueryingBase<TextEditFormControl>
{
    public TextFieldQuerying() : base()
    {
    }
    public override IEnumerable<Entry> Query([NotNull] IEnumerable<Entry> source, [NotNull] QueryingByField customField)
    {
        return source.Where(e => e.ExtraProperties.ContainsKey(customField.Name) &&
            e.ExtraProperties[customField.Name].ToString().Contains(customField.Value)
        );
    }
}
