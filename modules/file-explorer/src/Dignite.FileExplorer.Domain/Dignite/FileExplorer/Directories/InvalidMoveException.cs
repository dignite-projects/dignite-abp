using System.Runtime.Serialization;
using Volo.Abp;

namespace Dignite.FileExplorer.Directories;

public class InvalidMoveException : BusinessException
{
    public InvalidMoveException()
    {
        Code = FileExplorerErrorCodes.Directories.CantMovableToUnderChild;
    }

    public InvalidMoveException(SerializationInfo serializationInfo, StreamingContext context)
        : base(serializationInfo, context)
    {
        Code = FileExplorerErrorCodes.Directories.CantMovableToUnderChild;
    }
}