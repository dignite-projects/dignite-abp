namespace Dignite.Abp.MultiTenancyDomains;

public static class MultiTenancyDomainsDbProperties
{
    public static string DbTablePrefix { get; set; } = "Abp";

    public static string? DbSchema { get; set; } = null;

    public const string ConnectionStringName = "AbpMultiTenancyDomains";
}
