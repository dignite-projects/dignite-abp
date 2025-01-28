using Volo.Abp.Reflection;

namespace Dignite.Cms.Permissions
{
    public class CmsPermissions
    {
        public const string GroupName = "CmsCommon";

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(CmsPermissions));
        }
    }
}