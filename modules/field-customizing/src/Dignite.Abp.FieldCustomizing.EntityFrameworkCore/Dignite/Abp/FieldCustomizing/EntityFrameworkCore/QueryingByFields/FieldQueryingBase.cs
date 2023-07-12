using System;
using System.Collections.Generic;
using Dignite.Abp.DynamicForms;
using JetBrains.Annotations;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.FieldCustomizing.EntityFrameworkCore.QueryingByFields;
public abstract class FieldQueryingBase<TForm> : IFieldQuerying, ITransientDependency
    where TForm : IForm
{

    protected FieldQueryingBase()
    {
        FormType = typeof(TForm);
    }

    public Type FormType { get; private set; }


    public abstract IEnumerable<TSource> Query<TSource>([NotNull] IEnumerable<TSource> source, [NotNull] QueryingByFieldParameter parameter)
        where TSource : IHasCustomFields;
}
