using FileExplorerSample.Localization;
using Volo.Abp.AspNetCore.Components;

namespace FileExplorerSample;

public abstract class FileExplorerSampleComponentBase : AbpComponentBase
{
    protected FileExplorerSampleComponentBase()
    {
        LocalizationResource = typeof(FileExplorerSampleResource);
    }
}
