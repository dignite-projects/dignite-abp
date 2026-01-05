using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Extensions.Options;
using Volo.Abp;

namespace Dignite.Abp.UserPoints;

public class DefaultUserPointTypeDefinitionStore : IUserPointTypeDefinitionStore
{
    protected UserPointOptions Options { get; }

    public DefaultUserPointTypeDefinitionStore(IOptions<UserPointOptions> options)
    {
        Options = options.Value;
    }

    public virtual Task<UserPointTypeDefinition> GetAsync([NotNull] string pointTypeName)
    {
        Check.NotNullOrWhiteSpace(pointTypeName, nameof(pointTypeName));

        var result = Options.PointTypes.SingleOrDefault(x => x.Name.Equals(pointTypeName, StringComparison.InvariantCultureIgnoreCase)) ??
                     throw new UnsupportedPointTypeException(pointTypeName);

        return Task.FromResult(result);
    }

    public virtual Task<List<UserPointTypeDefinition>> GetAllAsync()
    {
        return Task.FromResult(Options.PointTypes);
    }

    public virtual Task<bool> IsDefinedAsync([NotNull] string pointTypeName)
    {
        Check.NotNullOrWhiteSpace(pointTypeName, nameof(pointTypeName));

        var isDefined = Options.PointTypes.Any(x => x.Name == pointTypeName);

        return Task.FromResult(isDefined);
    }
}
