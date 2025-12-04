using System;
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

    public virtual Task<UserPointTypeDefinition> GetAsync([NotNull] string pointType)
    {
        Check.NotNullOrWhiteSpace(pointType, nameof(pointType));

        var result = Options.PointTypes.SingleOrDefault(x => x.PointType.Equals(pointType, StringComparison.InvariantCultureIgnoreCase)) ??
                     throw new UnsupportedPointTypeException(pointType);

        return Task.FromResult(result);
    }

    public virtual Task<bool> IsDefinedAsync([NotNull] string pointType)
    {
        Check.NotNullOrWhiteSpace(pointType, nameof(pointType));

        var isDefined = Options.PointTypes.Any(x => x.PointType == pointType);

        return Task.FromResult(isDefined);
    }
}
