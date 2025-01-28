using System;
using System.Collections.Generic;
using Dignite.Abp.DynamicForms;
using Dignite.Cms.Entries;
using JetBrains.Annotations;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.Data;
public abstract class FieldQueryingBase<TFormControl> : IFieldQuerying, ITransientDependency
    where TFormControl : IFormControl
{

    protected FieldQueryingBase()
    {
        FormControlType = typeof(TFormControl);
    }

    public Type FormControlType { get; private set; }


    public abstract IEnumerable<Entry> Query([NotNull] IEnumerable<Entry> source, [NotNull] QueryingByField customField);
}
