namespace Dignite.Publisher;

public static class PublisherErrorCodes
{
    public static class Categories
    {
        public const string NameAlreadyExist = "Publisher:Category:0001";
        public const string NotFound = "Publisher:Category:0002";
    }
    public static class Posts
    {
        public const string SlugAlreadyExist = "Publisher:Post:0001";
    }
}
