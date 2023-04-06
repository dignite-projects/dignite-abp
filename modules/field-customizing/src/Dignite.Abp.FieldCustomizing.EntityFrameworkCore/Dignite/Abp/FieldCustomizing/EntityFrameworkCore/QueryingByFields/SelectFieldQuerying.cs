using System.Collections.Generic;
using System.Linq;
using Dignite.Abp.DynamicForms.Select;
using JetBrains.Annotations;
using Dignite.Abp.DynamicForms;

namespace Dignite.Abp.FieldCustomizing.EntityFrameworkCore.QueryingByFields;
public class SelectFieldQuerying : FieldQueryingBase<SelectForm>
{

    public SelectFieldQuerying():base()
    {
    }


    public override IEnumerable<TSource> Query<TSource>([NotNull] IEnumerable<TSource> source, [NotNull] QueryingByFieldParameter parameter)
    {
        var value = parameter.Value.Split(',');
        return source.Where(e => e.CustomFields.ContainsKey(parameter.FieldName) &&
            e.GetField<string[]>(parameter.FieldName).Any(v=> value.Contains(v))
        );
    }
}
