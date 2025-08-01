using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dignite.Abp.LocaleManagement;
public class UpdateLocaleInput
{
    [Required]
    [StringLength(16)]
    public string DefaultCultureName { get; set; }

    [Required]
    public IEnumerable<string> AvailableCultureNames { get; set; }
}
