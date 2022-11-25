using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace Dignite.FileExplorer.Directories;

public class GetDirectoriesInput : PagedResultRequestDto
{
    public Guid? CreatorId { get; set; }

    [Required]
    public string ContainerName { get; set; }

    public Guid? ParentId { get; set; }
}