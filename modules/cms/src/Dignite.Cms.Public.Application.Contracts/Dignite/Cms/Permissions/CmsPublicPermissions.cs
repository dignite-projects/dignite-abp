using Volo.Abp.Reflection;

namespace Dignite.Cms.Permissions
{
    public class CmsPublicPermissions
    {
        public const string GroupName = "CmsPublic";

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(CmsPublicPermissions));
        }
    }
}