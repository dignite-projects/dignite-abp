using Volo.Abp.Reflection;

namespace Dignite.Publisher.Public.Permissions;

public class PublisherPublicPermissions
{
    public const string GroupName = "PublisherPublic";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(PublisherPublicPermissions));
    }
}
