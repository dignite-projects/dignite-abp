using System;
using System.Collections.Generic;
using System.Linq;
using Dignite.Abp.DynamicForms.Switch;
using JetBrains.Annotations;

namespace Dignite.Abp.FieldCustomizing.EntityFrameworkCore.QueryingByFields;
public class SwitchFieldQuerying : FieldQueryingBase<SwitchForm>
{
    public SwitchFieldQuerying():base()
    { }


    public override IEnumerable<TSource> Query<TSource>([NotNull] IEnumerable<TSource> source, [NotNull] QueryingByFieldParameter parameter)
    {
        var value = bool.Parse(parameter.Value);
        return source.Where(e => e.CustomFields.ContainsKey(parameter.FieldName)
            && Convert.ToBoolean(e.CustomFields[parameter.FieldName]) == value
        );
    }
}
