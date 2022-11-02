using System.Runtime.Serialization;
using Volo.Abp;

namespace Dignite.FileExplorer.Directories;

public class DirectoryAlreadyExistException : BusinessException
{
    public DirectoryAlreadyExistException(SerializationInfo serializationInfo, StreamingContext context)
        : base(serializationInfo, context)
    {
    }

    public DirectoryAlreadyExistException(string directoryName)
    {
        DirectoryName = directoryName;

        Code = FileExplorerErrorCodes.Directories.DirectoryAlreadyExist;

        WithData(nameof(DirectoryName), DirectoryName);
    }

    public virtual string DirectoryName { get; }
}