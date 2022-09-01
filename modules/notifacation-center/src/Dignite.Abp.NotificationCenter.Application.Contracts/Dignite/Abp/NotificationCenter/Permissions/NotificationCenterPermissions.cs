using Volo.Abp.Reflection;

namespace Dignite.Abp.NotificationCenter.Permissions
{
    public class NotificationCenterPermissions
    {
        public const string GroupName = "NotificationCenter";

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(NotificationCenterPermissions));
        }
    }
}