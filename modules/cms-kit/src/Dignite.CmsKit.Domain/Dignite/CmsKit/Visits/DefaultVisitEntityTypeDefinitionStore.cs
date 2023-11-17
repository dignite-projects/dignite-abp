using JetBrains.Annotations;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;

namespace Dignite.CmsKit.Visits;

public class DefaultVisitEntityTypeDefinitionStore : IVisitEntityTypeDefinitionStore
{
    protected CmsKitVisitOptions Options { get; }

    public DefaultVisitEntityTypeDefinitionStore(IOptions<CmsKitVisitOptions> options)
    {
        Options = options.Value;
    }

    public virtual Task<VisitEntityTypeDefinition> GetAsync([NotNull] string entityType)
    {
        Check.NotNullOrWhiteSpace(entityType, nameof(entityType));

        var definition = Options.EntityTypes.SingleOrDefault(x => x.EntityType.Equals(entityType, StringComparison.InvariantCultureIgnoreCase)) ??
                     throw new EntityCantHaveVisitException(entityType);

        return Task.FromResult(definition);
    }

    public virtual Task<bool> IsDefinedAsync([NotNull] string entityType)
    {
        Check.NotNullOrWhiteSpace(entityType, nameof(entityType));

        var isDefined = Options.EntityTypes.Any(x => x.EntityType.Equals(entityType, StringComparison.InvariantCultureIgnoreCase));

        return Task.FromResult(isDefined);
    }
}
