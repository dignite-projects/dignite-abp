namespace Dignite.Cms;

public static class CmsErrorCodes
{
    public static class Sections
    {
        public const string NameAlreadyExist = "Cms:Section:0001";
        public const string DefaultSectionCannotSetNotActive = "Cms:Section:0002";
        public const string DefaultSectionMustBeSingleType = "Cms:Section:0003";
        public const string RouteMissingSlugRoutingParameter = "Cms:Section:0004";
        public const string RouteAlreadyExist = "Cms:Section:0005";
    }
    public static class EntryTypes
    {
        public const string NameAlreadyExist = "Cms:EntryType:0001";
    }
    public static class Fields
    {
        public const string NameAlreadyExist = "Cms:Field:0001";
    }

    public static class Entries
    {
        public const string SlugAlreadyExist = "Cms:Entry:0001";
        public const string CultureAlreadyExist = "Cms:Entry:0002";
        public const string InformationInconsistent = "Cms:Entry:0003";
    }
}
