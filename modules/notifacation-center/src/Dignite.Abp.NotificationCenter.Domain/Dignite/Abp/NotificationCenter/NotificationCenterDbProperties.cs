namespace Dignite.Abp.NotificationCenter
{
    public static class NotificationCenterDbProperties
    {
        public static string DbTablePrefix { get; set; } = "Di";

        public static string DbSchema { get; set; } = null;

        public const string ConnectionStringName = "NotificationCenter";
    }
}
