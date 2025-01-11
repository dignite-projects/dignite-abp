using System.ComponentModel.DataAnnotations;
using Volo.Abp.Validation;

namespace Dignite.Abp.TenantDomains;

public class UpdateDomainInput
{
    [Required]
    [DynamicMaxLength(typeof(TenantDomainConsts), nameof(TenantDomainConsts.MaxDomainNameLength))]
    [RegularExpression(TenantDomainConsts.NameRegularExpression)]
    public string DomainName { get; set; }
}
