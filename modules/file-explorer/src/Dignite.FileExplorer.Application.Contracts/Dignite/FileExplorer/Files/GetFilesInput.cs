using System;
using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;
using Volo.Abp.Application.Dtos;

namespace Dignite.FileExplorer.Files;

public class GetFilesInput : PagedAndSortedResultRequestDto
{
    [Required]
    public string ContainerName { get; set; }

    public Guid? DirectoryId { get; set; }

    public Guid? CreatorId { get; set; }

    [CanBeNull]
    public string? Filter { get; set; }

    [CanBeNull]
    public string? EntityId { get; set; }
}