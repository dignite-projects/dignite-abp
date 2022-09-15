using System;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.Files.Fakes;
public class FakeFile : IFile, ITransientDependency
{

    public FakeFile(string containerName, string blobName, long size, string name, string mimeType)
    {
        ContainerName = containerName;
        BlobName = blobName;
        Size = size;
        Name = name;
        MimeType = mimeType;
    }

    public string ContainerName { get; set; }

    public string BlobName { get; set; }

    public long Size { get; set; }

    public string Name { get; set; }

    public string MimeType { get; set; }

    public Guid Id { get; set; }

    public Guid? TenantId { get; set; }

    public object[] GetKeys()
    {
        return new object[] { Id };
    }

    public void Resize(long size)
    {
        this.Size = size;   
    }
}
