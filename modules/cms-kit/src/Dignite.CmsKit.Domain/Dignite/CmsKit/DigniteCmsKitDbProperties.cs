using Volo.Abp.Data;

namespace Dignite.CmsKit;

public static class DigniteCmsKitDbProperties
{
    public static string DbTablePrefix { get; set; } = "Cms";

    public static string DbSchema { get; set; } = AbpCommonDbProperties.DbSchema;

    public const string ConnectionStringName = "CmsKit";
}
