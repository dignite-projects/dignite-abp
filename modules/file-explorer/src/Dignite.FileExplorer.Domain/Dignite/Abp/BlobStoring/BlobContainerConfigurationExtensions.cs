using System;
using Volo.Abp.BlobStoring;
using Volo.Abp.Collections;

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


    #region Image resize handler configuration extenisions

    public static ImageResizeHandlerConfiguration GetImageResizeConfiguration(
        this BlobContainerConfiguration containerConfiguration)
    {
        return new ImageResizeHandlerConfiguration(containerConfiguration);
    }

    public static void AddImageResizeHandler(
        this BlobContainerConfiguration containerConfiguration,
        Action<ImageResizeHandlerConfiguration> configureAction)
    {
        var blobProcessHandlers = containerConfiguration.GetConfigurationOrDefault(
            BlobContainerConfigurationNames.FileHandlers,
            new TypeList<IFileHandler>());

        if (blobProcessHandlers.TryAdd<ImageResizeHandler>())
        {
            configureAction(new ImageResizeHandlerConfiguration(containerConfiguration));

            containerConfiguration.SetConfiguration(
                BlobContainerConfigurationNames.FileHandlers,
                blobProcessHandlers);
        }
    }

    #endregion Image resize handler configuration extenisions
}