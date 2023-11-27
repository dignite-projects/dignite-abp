using System;
using Dignite.Abp.Files;
using Volo.Abp.Auditing;
using Volo.Abp.MultiTenancy;

namespace Dignite.FileExplorer.Files;

public class FileDescriptor : FileBase, ICreationAuditedObject, IDeletionAuditedObject, IMultiTenant
{
    protected FileDescriptor()
    { }

    public FileDescriptor(Guid id, string containerName, string blobName, string name, string mimeType, string cellName, Guid? directoryId, string entityId, Guid? tenantId)
        : base(id, containerName, blobName, name, mimeType)
    {
        CellName = cellName;
        DirectoryId = directoryId;
        EntityId = entityId;
        TenantId = tenantId;
    }

    public string CellName { get; set; }

    /// <summary>
    /// Directory in container
    /// </summary>
    public Guid? DirectoryId { get; set; }

    /// <summary>
    /// Associated Entity Id
    /// </summary>
    public string EntityId { get; protected set; }

    public DateTime CreationTime { get; set; }

    public Guid? CreatorId { get; set; }

    public Guid? DeleterId { get; set; }
    public DateTime? DeletionTime { get; set; }
    public bool IsDeleted { get; set; }

    public Guid? TenantId { get; protected set; }
}