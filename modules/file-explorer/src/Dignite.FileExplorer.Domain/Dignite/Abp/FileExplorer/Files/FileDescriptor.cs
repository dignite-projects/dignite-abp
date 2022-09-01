using System;
using Dignite.Abp.Files;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;

namespace Dignite.FileExplorer.Files;

public class FileDescriptor : BasicAggregateRoot<Guid>, IFile, ICreationAuditedObject, IDeletionAuditedObject, IMultiTenant
{
    protected FileDescriptor()
    { }

    public FileDescriptor(Guid id, string entityTypeFullName, string entityId, BasicFile blobInfo, string name, string mineType, Guid? tenantId)
    {
        Id = id;
        EntityTypeFullName = entityTypeFullName;
        EntityId = entityId;
        SetBlobInfo(blobInfo);
        Name = name;
        MimeType = mineType;
        TenantId = tenantId;
    }

    #region blob info
    public string ContainerName { get; set; }

    public string BlobName { get; set; }

    public long Size { get; set; }
    #endregion

    public string EntityTypeFullName { get; set; }

    public string EntityId { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string Name { get; set; }

    public string MimeType { get; set; }

    public DateTime CreationTime { get; set; }

    public Guid? CreatorId { get; set; }

    public Guid? DeleterId { get; set; }
    public DateTime? DeletionTime { get; set; }
    public bool IsDeleted { get; set; }

    public Guid? TenantId { get; protected set; }

    private void SetBlobInfo(BasicFile blobInfo)
    {
        ContainerName = blobInfo.ContainerName;
        BlobName = blobInfo.BlobName;
        Size = blobInfo.Size;
    }
}
