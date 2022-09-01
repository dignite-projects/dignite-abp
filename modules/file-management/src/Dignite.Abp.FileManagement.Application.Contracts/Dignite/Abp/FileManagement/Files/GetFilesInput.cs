using Volo.Abp.Application.Dtos;

namespace Dignite.Abp.FileManagement.Files;

public class GetFilesInput: PagedAndSortedResultRequestDto
{
    public string ContainerName { get; set; }
    public string Filter { get; set; }

    public string EntityTypeFullName { get; set; } 
    public string EntityId { get; set; }
}
