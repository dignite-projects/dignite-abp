using System.Runtime.Serialization;
using Dignite.FileExplorer;
using Volo.Abp;

namespace Dignite.Abp.FileExplorer.Directories;
public class InvalidMoveException : BusinessException
{
    public InvalidMoveException(SerializationInfo serializationInfo, StreamingContext context)
        : base(serializationInfo, context)
    {
        Code = FileExplorerErrorCodes.Directories.CantMovableToUnderChild;
    }
}
