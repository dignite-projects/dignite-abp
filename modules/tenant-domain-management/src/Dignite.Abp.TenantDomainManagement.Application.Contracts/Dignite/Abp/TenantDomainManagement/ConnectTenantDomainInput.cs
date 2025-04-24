using System.ComponentModel.DataAnnotations;
using Volo.Abp.Validation;

namespace Dignite.Abp.TenantDomainManagement;

public class ConnectTenantDomainInput
{
    /// <summary>
    /// 用户自定义域名
    /// </summary>
    [Required]
    [DynamicMaxLength(typeof(TenantDomainConsts), nameof(TenantDomainConsts.MaxDomainNameLength))]
    [RegularExpression(TenantDomainConsts.NameRegularExpression)]
    public string DomainName { get; set; } = null!;
}
