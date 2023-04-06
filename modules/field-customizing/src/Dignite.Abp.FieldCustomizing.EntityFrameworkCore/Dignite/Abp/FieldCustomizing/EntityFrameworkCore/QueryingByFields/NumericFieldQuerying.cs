using System;
using System.Collections.Generic;
using System.Linq;
using Dignite.Abp.DynamicForms.NumericEdit;
using JetBrains.Annotations;

namespace Dignite.Abp.FieldCustomizing.EntityFrameworkCore.QueryingByFields;
public class NumericFieldQuerying : FieldQueryingBase<NumericEditForm>
{
    public NumericFieldQuerying():base()
    { }


    public override IEnumerable<TSource> Query<TSource>([NotNull] IEnumerable<TSource> source, [NotNull] QueryingByFieldParameter parameter)
    {
        var min = double.Parse(parameter.Value.Split('-')[0]);
        var max = double.Parse(parameter.Value.Split('-')[1]);
        return source.Where(e => e.CustomFields.ContainsKey(parameter.FieldName)
            && Convert.ToDouble(e.CustomFields[parameter.FieldName]) >= min
            && Convert.ToDouble(e.CustomFields[parameter.FieldName]) < max
        );
    }
}
