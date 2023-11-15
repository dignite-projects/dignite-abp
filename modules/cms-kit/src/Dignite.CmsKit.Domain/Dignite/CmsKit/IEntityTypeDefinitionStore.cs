using JetBrains.Annotations;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Dignite.CmsKit;

public interface IEntityTypeDefinitionStore<TPolicyDefinition> : ITransientDependency
    where TPolicyDefinition : EntityTypeDefinition
{
    Task<TPolicyDefinition> GetAsync([NotNull] string entityType);

    Task<bool> IsDefinedAsync([NotNull] string entityType);
}
