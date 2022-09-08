using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Dignite.Abp.FieldCustomizing;

public interface ICustomizeFieldDefinitionRepository<TFieldDefinition> : IBasicRepository<TFieldDefinition, Guid>
    where TFieldDefinition : CustomizeFieldDefinitionBase
{
    Task<bool> NameExistsAsync(string name, Guid? ignoredId = null, CancellationToken cancellationToken = default);

    Task<List<TFieldDefinition>> GetListAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);
}
