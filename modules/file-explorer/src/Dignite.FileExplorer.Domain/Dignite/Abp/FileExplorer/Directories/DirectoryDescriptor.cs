using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Dignite.Abp.FileExplorer.Directories;

public class DirectoryDescriptor: AuditedAggregateRoot<Guid>, IMultiTenant
{
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
