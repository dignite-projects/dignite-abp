﻿namespace Dignite.Abp.TenantDomains;

public static class TenantDomainsDbProperties
{
    public static string DbTablePrefix { get; set; } = "Abp";

    public static string? DbSchema { get; set; } = null;

    public const string ConnectionStringName = "AbpTenantDomains";
}
