namespace Dignite.Abp.TenantDomains;

public static class TenantDomainConsts
{
    /// <summary>
    /// Maximum length of the tenant domain name property.
    /// Default value: 128
    /// </summary>
    public static int MaxDomainNameLength { get; set; } = 128;


    /// <summary>
    /// Regular Expression of the Name property.
    /// </summary>
    public const string NameRegularExpression = @"^((?!-)[A-Za-z0-9-]{1,63}(?<!-)\.)+[A-Za-z]{2,6}(:\d{1,5})?$";
}
