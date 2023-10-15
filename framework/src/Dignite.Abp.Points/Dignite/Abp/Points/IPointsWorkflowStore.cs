using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Dignite.Abp.Points;
public interface IPointsWorkflowStore
{
    Task<PointsWorkflow> GetOrNullAsync(
        [NotNull] string pointsDefinitionName,
        [NotNull] string workflowName
    );
}
