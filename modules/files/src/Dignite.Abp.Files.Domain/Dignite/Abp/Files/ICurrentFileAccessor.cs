namespace Dignite.Abp.Files;

public interface ICurrentFileAccessor
{
    IFile Current { get; set; }
}