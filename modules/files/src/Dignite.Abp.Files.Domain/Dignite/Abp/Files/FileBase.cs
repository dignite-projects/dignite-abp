using System;
using Volo.Abp.Domain.Entities;

namespace Dignite.Abp.Files;

public abstract class FileBase : AggregateRoot<Guid>, IFile
{
    protected FileBase()
    {
    }

    protected FileBase(Guid id, string containerName, string blobName, string name, string mimeType) : base(id)
    {
        ContainerName = containerName;
        BlobName = blobName;
        Name = name;
        MimeType = mimeType;
    }

    /// <summary>
    /// Container name of blob
    /// </summary>
    public string ContainerName { get; protected set; }

    /// <summary>
    /// Blob name
    /// </summary>
    public string BlobName { get; protected set; }

    /// <summary>
    /// Blob binary size
    /// </summary>
    public long Size { get; protected set; }

    /// <summary>
    /// File name
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// File mime type
    /// </summary>
    public string MimeType { get; protected set; }

    /// <summary>
    /// File md5
    /// </summary>
    public string Md5 { get; protected set; }

    /// <summary>
    /// Referencing other blob
    /// </summary>
    public string ReferBlobName { get; protected set; }

    public void SetMd5(string md5)
    {
        Md5 = md5;
    }

    public void SetReferBlobName(string blobName)
    {
        ReferBlobName = BlobName;
    }

    public void SetSize(long size)
    {
        Size = size;
    }
}