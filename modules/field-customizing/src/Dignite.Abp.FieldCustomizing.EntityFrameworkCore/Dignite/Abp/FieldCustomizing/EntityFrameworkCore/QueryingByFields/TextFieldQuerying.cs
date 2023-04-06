using System.Collections.Generic;
using System.Linq;
using Dignite.Abp.DynamicForms.Textbox;
using JetBrains.Annotations;

namespace Dignite.Abp.FieldCustomizing.EntityFrameworkCore.QueryingByFields;
public class TextFieldQuerying : FieldQueryingBase<TextboxForm>
{
    public TextFieldQuerying():base()
    {
    }
    public override IEnumerable<TSource> Query<TSource>([NotNull] IEnumerable<TSource> source, [NotNull] QueryingByFieldParameter parameter)
    {
        return source.Where(e => e.CustomFields.ContainsKey(parameter.Value) &&
            (e.CustomFields[parameter.FieldName]).ToString().Contains(parameter.Value)
        );
    }
}
