using Volo.Abp.BlobStoring;

namespace Dignite.Abp.BlobStoring;

public static class BlobContainerConfigurationExtensions
{
    public static void SetBlobNameGenerator<TBlobNameGenerator>(
        this BlobContainerConfiguration containerConfiguration)
        where TBlobNameGenerator : IBlobNameGenerator
    {
        containerConfiguration.SetConfiguration(
            BlobContainerConfigurationNames.BlobNamingGenerator,
            typeof(TBlobNameGenerator));
    }
}