using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace FileExplorerSample;

[Dependency(ReplaceServices = true)]
public class FileExplorerSampleBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "FileExplorerSample";
}
