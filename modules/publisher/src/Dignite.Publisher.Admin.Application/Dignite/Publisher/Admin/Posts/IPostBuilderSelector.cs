namespace Dignite.Publisher.Admin.Posts;
public interface IPostBuilderSelector
{
    IPostBuilder Get(string postTypeName);
}
