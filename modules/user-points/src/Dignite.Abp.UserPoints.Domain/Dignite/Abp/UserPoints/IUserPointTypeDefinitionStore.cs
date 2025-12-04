using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.UserPoints;

public interface IUserPointTypeDefinitionStore : ITransientDependency
{
    Task<UserPointTypeDefinition> GetAsync([NotNull] string pointType);

    Task<bool> IsDefinedAsync([NotNull] string pointType);
}
