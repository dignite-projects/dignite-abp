using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.UserPoints;

public interface IUserPointEntityTypeDefinitionStore : ITransientDependency
{
    Task<UserPointEntityTypeDefinition> GetAsync([NotNull] string entityType);

    Task<bool> IsDefinedAsync([NotNull] string entityType);
}
