using Volo.Abp.Reflection;

namespace Dignite.Publisher.Permissions;

public class PublisherPermissions
{
    public const string GroupName = "PublisherCommon";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(PublisherPermissions));
    }
}
