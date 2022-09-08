using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Studio.ModuleInstalling;

namespace Dignite.Abp.FieldCustomizing;

[Dependency(ServiceLifetime.Transient, ReplaceServices = true)]
[ExposeServices(typeof(IModuleInstallingPipelineBuilder))]
public class FieldCustomizingInstallerPipelineBuilder : ModuleInstallingPipelineBuilderBase, IModuleInstallingPipelineBuilder, ITransientDependency
{
    public async Task<ModuleInstallingPipeline> BuildAsync(ModuleInstallingContext context)
    {
        context.AddEfCoreConfigurationMethodDeclaration(
            new EfCoreConfigurationMethodDeclaration(
                "Dignite.Abp.FieldCustomizing.EntityFrameworkCore",
                "ConfigureCustomizableFieldDefinitions"
            ),
            new EfCoreConfigurationMethodDeclaration(
                "Dignite.Abp.FieldCustomizing.EntityFrameworkCore",
                "ConfigureObjectCustomizedFields"
            )
        );
        return GetBasePipeline(context);
    }
}
