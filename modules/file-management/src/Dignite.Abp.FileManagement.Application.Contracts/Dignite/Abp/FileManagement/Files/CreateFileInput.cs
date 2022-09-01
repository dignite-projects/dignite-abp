using System.ComponentModel.DataAnnotations;
using Volo.Abp.Content;

namespace Dignite.Abp.FileManagement.Files;

public class CreateFileInput
{
    [Required]
    [StringLength(FileDescriptorConsts.MaxContainerNameLength)]
    public string ContainerName { get; set; }

    [Required]
    public IRemoteStreamContent File { get; set; }

    [Required]
    [StringLength(FileDescriptorConsts.MaxEntityTypeFullNameLength)]
    public string EntityTypeFullName { get; set; }

    [Required]
    [StringLength(FileDescriptorConsts.MaxEntityIdLength)]
    public string EntityId { get; set; }
}
