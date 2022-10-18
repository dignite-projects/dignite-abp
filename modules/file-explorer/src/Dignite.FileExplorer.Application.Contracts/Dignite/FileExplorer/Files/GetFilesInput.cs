using System;
using Volo.Abp.Application.Dtos;

namespace Dignite.FileExplorer.Files;

public class GetFilesInput : PagedAndSortedResultRequestDto
{
    public string ContainerName { get; set; }

    public Guid? DirectoryId { get; set; }

    public string Filter { get; set; }

    public string EntityTypeFullName { get; set; }
    public string EntityId { get; set; }
}