using System.ComponentModel.DataAnnotations;

namespace Dignite.CmsKit.Admin.Brand;

public class UpdateBrandInput
{
    /// <summary>
    /// Name of the site
    /// </summary>
    [Required]
    [MaxLength(32)]
    public string Name { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [MaxLength(128)]
    public string? LogoBlobName { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [MaxLength(128)]
    public string? LogoReverseBlobName { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [MaxLength(128)]
    public string? IconBlobName { get; set; }
}
