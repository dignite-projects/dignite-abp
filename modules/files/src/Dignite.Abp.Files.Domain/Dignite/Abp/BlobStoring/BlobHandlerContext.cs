using System.IO;
using Dignite.Abp.Files;
using Volo.Abp.BlobStoring;

namespace Dignite.Abp.BlobStoring;

public class BlobHandlerContext
{
    public BlobHandlerContext(
        IFile file,
        Stream blobStream,
        BlobContainerConfiguration containerConfiguration
        )
    {
        File = file;
        BlobStream = blobStream;
        ContainerConfiguration = containerConfiguration;
    }

    public IFile File { get; set; }

    public Stream BlobStream { get; set; }

    public BlobContainerConfiguration ContainerConfiguration { get; }
}