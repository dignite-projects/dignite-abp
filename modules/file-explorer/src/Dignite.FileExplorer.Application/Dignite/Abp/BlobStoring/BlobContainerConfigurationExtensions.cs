using System;
using Volo.Abp.BlobStoring;

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
}