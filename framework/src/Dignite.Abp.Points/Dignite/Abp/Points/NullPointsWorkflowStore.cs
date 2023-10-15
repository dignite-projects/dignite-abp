using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Settings;

namespace Dignite.Abp.Points;

[Dependency(TryRegister = true)]
public class NullPointsWorkflowStore : IPointsWorkflowStore, ISingletonDependency
{
    public ILogger<NullSettingStore> Logger { get; set; }

    public NullPointsWorkflowStore()
    {
        Logger = NullLogger<NullSettingStore>.Instance;
    }

    public Task<PointsWorkflow> GetOrNullAsync([NotNull] string pointsDefinitionName, [NotNull] string workflowName)
    {
        return Task.FromResult((PointsWorkflow)null);
    }
}
