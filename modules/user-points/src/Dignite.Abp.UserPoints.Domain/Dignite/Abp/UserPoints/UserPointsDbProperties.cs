using Volo.Abp.Data;

namespace Dignite.Abp.UserPoints;

public static class UserPointsDbProperties
{
    public static string DbTablePrefix { get; set; } = AbpCommonDbProperties.DbTablePrefix;

    public static string? DbSchema { get; set; } = AbpCommonDbProperties.DbSchema;

    public const string ConnectionStringName = "AbpUserPoints";
}
