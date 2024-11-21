using System;
using Volo.Abp.MultiTenancy;

namespace Dignite.FileExplorer.Files;

[Serializable]
public class FileDescriptorDto : FileDescriptorListDto, IMultiTenant
{
    public string Url { get; set; }

    public Guid? TenantId { get; set; }
}