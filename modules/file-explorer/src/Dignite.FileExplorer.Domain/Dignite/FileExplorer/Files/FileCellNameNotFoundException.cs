using System.Runtime.Serialization;
using Volo.Abp;

namespace Dignite.FileExplorer.Files;
public class FileCellNameNotFoundException : BusinessException
{
    public FileCellNameNotFoundException()
    {
        Code = FileExplorerErrorCodes.Files.CellNameNotFound;
    }

    public FileCellNameNotFoundException(SerializationInfo serializationInfo, StreamingContext context)
        : base(serializationInfo, context)
    {
        Code = FileExplorerErrorCodes.Files.CellNameNotFound;
    }
}