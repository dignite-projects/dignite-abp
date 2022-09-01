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
            BlobContainerConfigurationNames.BlobNamingGenerator,
            typeof(TBlobNameGenerator));
    }

    #region Blob size limit handler configuration extenisions
    public static BlobSizeLimitHandlerConfiguration GetBlobSizeLimitConfiguration(
        this BlobContainerConfiguration containerConfiguration)
    {
        return new BlobSizeLimitHandlerConfiguration(containerConfiguration);
    }

    public static void AddBlobSizeLimitHandler(
        this BlobContainerConfiguration containerConfiguration,
        Action<BlobSizeLimitHandlerConfiguration> configureAction)
    {
        var blobProcessHandlers = containerConfiguration.GetConfigurationOrDefault(
            BlobContainerConfigurationNames.BlobHandlers,
            new TypeList<IBlobHandler>());


        if (blobProcessHandlers.TryAdd<BlobSizeLimitHandler>())
        {
            configureAction(new BlobSizeLimitHandlerConfiguration(containerConfiguration));

            containerConfiguration.SetConfiguration(
                BlobContainerConfigurationNames.BlobHandlers,
                blobProcessHandlers);
        }
    }
    #endregion
}
