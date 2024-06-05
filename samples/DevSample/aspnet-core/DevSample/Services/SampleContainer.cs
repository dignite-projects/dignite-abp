using Volo.Abp.BlobStoring;

namespace DevSample.Services;


[BlobContainerName(SampleContainerName)]
public class SampleContainer
{
    public const string SampleContainerName = "SampleContainer";
}
