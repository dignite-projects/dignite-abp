using System;
using Volo.Abp.BlobStoring;
using Volo.Abp.Collections;

namespace Dignite.Abp.BlobStoring;

public static class BlobContainerConfigurationExtensions
{
    public static BlobContainerAuthorizationConfiguration GetAuthorizationConfiguration(
        this BlobContainerConfiguration containerConfiguration)
    {
        return new BlobContainerAuthorizationConfiguration(containerConfiguration);
    }

    public static void SetAuthorizationConfiguration(
        this BlobContainerConfiguration containerConfiguration,
        Action<BlobContainerAuthorizationConfiguration> configureAction = null)
    {
        if (configureAction != null)
            configureAction(new BlobContainerAuthorizationConfiguration(containerConfiguration));
    }


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
}