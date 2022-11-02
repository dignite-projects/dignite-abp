using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace Dignite.FileExplorer.Files;

public class GetFilesInput : PagedAndSortedResultRequestDto
{
    [Required]
    public string ContainerName { get; set; }

    public Guid? DirectoryId { get; set; }

    public Guid? CreatorId { get; set; }

    public string Filter { get; set; }

    public string EntityType { get; set; }
    public string EntityId { get; set; }
}