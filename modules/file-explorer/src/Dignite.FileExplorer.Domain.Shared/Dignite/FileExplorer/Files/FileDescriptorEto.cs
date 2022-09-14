using System;
using Volo.Abp.Data;
using Volo.Abp.EventBus;
using Volo.Abp.MultiTenancy;

namespace Dignite.FileExplorer.Files;

[EventName("Dignite.FileExplorer.Files.FileDescriptor")]
[Serializable]
public class FileDescriptorEto : IMultiTenant, IHasExtraProperties
{
    public string ContainerName { get; set; }

    public string BlobName { get; set; }

    public long Size { get; set; }

    public string EntityTypeFullName { get; set; }

    public string EntityId { get; set; }

    /// <summary>
    ///
    /// </summary>
    public string Name { get; set; }

    public string MimeType { get; set; }

    public Guid? TenantId { get; set; }

    public virtual ExtraPropertyDictionary ExtraProperties {
        get;
        set;
    }
}