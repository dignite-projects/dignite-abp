using System.Runtime.Serialization;
using Volo.Abp;

namespace Dignite.FileExplorer.Directories;

public class DirectoryInvalidMoveException : BusinessException
{
    public DirectoryInvalidMoveException()
    {
        Code = FileExplorerErrorCodes.Directories.InvalidMove;
    }

    public DirectoryInvalidMoveException(SerializationInfo serializationInfo, StreamingContext context)
        : base(serializationInfo, context)
    {
        Code = FileExplorerErrorCodes.Directories.InvalidMove;
    }
}