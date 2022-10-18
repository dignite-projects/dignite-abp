using System;
using Dignite.Abp.Files;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;

namespace Dignite.FileExplorer.Files;

public class FileDescriptor : AggregateRoot<Guid>, IFile, ICreationAuditedObject, IDeletionAuditedObject, IMultiTenant
{
    protected FileDescriptor()
    { }

    public FileDescriptor(Guid id, string containerName, string blobName, Guid? directoryId, long size, string name, string mineType, string entityTypeFullName, string entityId, Guid? tenantId)
    {
        Id = id;
        ContainerName = containerName;
        BlobName = blobName;
        DirectoryId = directoryId;
        Size = size;
        Name = name;
        MimeType = mineType;
        EntityTypeFullName = entityTypeFullName;
        EntityId = entityId;
        TenantId = tenantId;
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
    /// Directory in container
    /// </summary>
    public Guid? DirectoryId { get; set; }

    /// <summary>
    /// Blob binary size
    /// </summary>
    public long Size { get; protected set; }

    /// <summary>
    /// File name
    /// </summary>
    public string Name { get; protected set; }

    /// <summary>
    /// File mime type
    /// </summary>
    public string MimeType { get; protected set; }

    /// <summary>
    /// Associated Entity Type Name
    /// </summary>
    public string EntityTypeFullName { get; protected set; }

    /// <summary>
    /// Associated Entity Key
    /// </summary>
    public string EntityId { get; protected set; }

    public DateTime CreationTime { get; set; }

    public Guid? CreatorId { get; set; }

    public Guid? DeleterId { get; set; }
    public DateTime? DeletionTime { get; set; }
    public bool IsDeleted { get; set; }

    public Guid? TenantId { get; protected set; }

    public void Resize(long size)
    {
        this.Size = size;
    }
}