using Volo.Abp;

namespace Dignite.FileExplorer.Directories;

public class DirectoryInvalidMoveException : BusinessException
{
    public DirectoryInvalidMoveException()
    {
        Code = FileExplorerErrorCodes.Directories.InvalidMove;
    }

}