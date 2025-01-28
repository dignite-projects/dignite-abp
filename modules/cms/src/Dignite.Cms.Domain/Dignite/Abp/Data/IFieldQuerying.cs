using Dignite.Cms.Entries;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.Data;
public interface IFieldQuerying : ITransientDependency
{
    Type FormControlType { get; }

    IEnumerable<Entry> Query([NotNull] IEnumerable<Entry> source, [NotNull] QueryingByField customField);
}
