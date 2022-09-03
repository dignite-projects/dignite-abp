using System.IO;
using Volo.Abp.BlobStoring;

namespace Dignite.Abp.BlobStoring;

public class BlobHandlerContext
{
    public BlobHandlerContext(
        Stream blobStream,
        BlobContainerConfiguration containerConfiguration
        )
    {
        BlobStream = blobStream;
        ContainerConfiguration = containerConfiguration;
    }



    public Stream BlobStream { get; set; }

    public BlobContainerConfiguration ContainerConfiguration { get; }
}
