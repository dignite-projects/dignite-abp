using Volo.Abp;

namespace Dignite.FileExplorer.Directories;

public class DirectoryAlreadyExistException : BusinessException
{
    public DirectoryAlreadyExistException(string directoryName)
    {
        DirectoryName = directoryName;

        Code = FileExplorerErrorCodes.Directories.DirectoryAlreadyExist;

        WithData(nameof(DirectoryName), DirectoryName);
    }

    public virtual string DirectoryName { get; }
}