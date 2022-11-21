using Volo.Abp.BlobStoring;

namespace FileExplorerSample.Services;


[BlobContainerName(SampleContainerName)]
public class SampleContainer
{
    public const string SampleContainerName = "SampleContainer";
}
