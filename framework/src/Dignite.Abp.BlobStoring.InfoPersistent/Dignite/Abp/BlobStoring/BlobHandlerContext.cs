using System;
using System.IO;
using Volo.Abp.BlobStoring;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.BlobStoring;

public class BlobHandlerContext: IServiceProviderAccessor
{
    public BlobHandlerContext(
        Stream blobStream,
        BlobContainerConfiguration containerConfiguration,
        IServiceProvider serviceProvider
        )
    {
        BlobStream = blobStream;
        ContainerConfiguration = containerConfiguration;
        ServiceProvider = serviceProvider;
    }



    public IServiceProvider ServiceProvider { get; }

    public Stream BlobStream { get; set; }

    public BlobContainerConfiguration ContainerConfiguration { get; }
}
