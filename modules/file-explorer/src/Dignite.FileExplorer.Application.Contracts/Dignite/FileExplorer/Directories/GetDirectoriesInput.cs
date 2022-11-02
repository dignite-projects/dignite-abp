using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace Dignite.FileExplorer.Directories;

public class GetDirectoriesInput : PagedResultRequestDto
{
    [Required]
    public string ContainerName { get; set; }

    public Guid? ParentId { get; set; }
}