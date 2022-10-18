using System.ComponentModel.DataAnnotations;
using Volo.Abp.Validation;

namespace Dignite.FileExplorer.Directories;
public class UpdateDirectoryInput
{
    /// <summary>
    /// 
    /// </summary>
    [Required]
    [DynamicMaxLength(typeof(DirectoryDescriptorConsts), nameof(DirectoryDescriptorConsts.MaxNameLength))]
    [RegularExpression(DirectoryDescriptorConsts.NameRegularExpression)]
    public string Name { get; set; }
}
