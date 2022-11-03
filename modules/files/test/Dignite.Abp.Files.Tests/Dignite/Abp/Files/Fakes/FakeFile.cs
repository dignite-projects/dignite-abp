using System;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.Files.Fakes;

public class FakeFile : FileBase, ITransientDependency
{
    public FakeFile(Guid id, string containerName, string blobName, string name, string mimeType)
        : base(id, containerName, blobName, name, mimeType)
    {
        ContainerName = containerName;
        BlobName = blobName;
        Name = name;
        MimeType = mimeType;
    }
}