using System.ComponentModel.DataAnnotations;

namespace Dignite.FileExplorer.Directories;

public class GetDirectoriesInput
{

    [Required]
    public string ContainerName { get; set; }
}