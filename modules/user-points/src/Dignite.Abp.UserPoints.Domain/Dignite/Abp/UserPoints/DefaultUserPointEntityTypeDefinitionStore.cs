using System;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Extensions.Options;
using Volo.Abp;

namespace Dignite.Abp.UserPoints;

public class DefaultUserPointEntityTypeDefinitionStore : IUserPointEntityTypeDefinitionStore
{
    protected UserPointOptions Options { get; }

    public DefaultUserPointEntityTypeDefinitionStore(IOptions<UserPointOptions> options)
    {
        Options = options.Value;
    }

    public virtual Task<UserPointEntityTypeDefinition> GetAsync([NotNull] string entityType)
    {
        Check.NotNullOrWhiteSpace(entityType, nameof(entityType));

        var result = Options.EntityTypes.SingleOrDefault(x => x.EntityType.Equals(entityType, StringComparison.InvariantCultureIgnoreCase)) ??
                     throw new EntityNotPointableException(entityType);

        return Task.FromResult(result);
    }

    public virtual Task<bool> IsDefinedAsync([NotNull] string entityType)
    {
        Check.NotNullOrWhiteSpace(entityType, nameof(entityType));

        var isDefined = Options.EntityTypes.Any(x => x.EntityType == entityType);

        return Task.FromResult(isDefined);
    }
}
