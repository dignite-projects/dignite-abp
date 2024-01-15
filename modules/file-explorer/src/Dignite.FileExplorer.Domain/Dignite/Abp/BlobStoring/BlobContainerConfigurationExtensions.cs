using System;
using Volo.Abp.BlobStoring;

namespace Dignite.Abp.BlobStoring;

public static class BlobContainerConfigurationExtensions
{
    public static void SetBlobNameGenerator<TBlobNameGenerator>(
        this BlobContainerConfiguration containerConfiguration)
        where TBlobNameGenerator : IBlobNameGenerator
    {
        containerConfiguration.SetConfiguration(
            FileExplorerBlobContainerConfigurationNames.BlobNamingGenerator,
            typeof(TBlobNameGenerator));
    }




    public static FileGridConfiguration GetFileGridConfiguration(
        this BlobContainerConfiguration containerConfiguration)
    {
        return new FileGridConfiguration(containerConfiguration);
    }

    public static void SetFileGridConfiguration(
        this BlobContainerConfiguration containerConfiguration,
        Action<FileGridConfiguration> configureAction = null)
    {
        if (configureAction != null)
            configureAction(new FileGridConfiguration(containerConfiguration));
    }

}