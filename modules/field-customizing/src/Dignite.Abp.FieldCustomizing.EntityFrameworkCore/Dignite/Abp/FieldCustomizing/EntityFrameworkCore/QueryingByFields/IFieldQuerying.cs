using System;
using System.Collections.Generic;
using Dignite.Abp.DynamicForms;
using JetBrains.Annotations;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.FieldCustomizing.EntityFrameworkCore.QueryingByFields;
public interface IFieldQuerying:ITransientDependency
{
    Type FormType { get; }

    IEnumerable<TSource> Query<TSource>([NotNull] IEnumerable<TSource> source,[NotNull] QueryingByFieldParameter parameter)
        where TSource : IHasCustomFields;
}
