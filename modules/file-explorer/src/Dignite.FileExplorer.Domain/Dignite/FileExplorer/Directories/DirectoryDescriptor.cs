using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Dignite.FileExplorer.Directories;

public class DirectoryDescriptor: AuditedAggregateRoot<Guid>, IMultiTenant
{
    public DirectoryDescriptor(Guid id, string containerName, string name, Guid? parentId, int order, Guid? tenantId)
        :base(id)
    {
        ContainerName = containerName;
        Name = name;
        ParentId = parentId;
        Order = order;
        TenantId = tenantId;
    }

    /// <summary>
    /// Container name of blob
    /// </summary>
    public string ContainerName { get; protected set; }

    /// <summary>
    /// 
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public Guid? ParentId { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int Order { get; set; }

    public Guid? TenantId { get; protected set; }


}
