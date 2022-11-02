using System.Runtime.Serialization;
using Volo.Abp;

namespace Dignite.FileExplorer.Directories;

public class InvalidDirectoryNameException : BusinessException
{
    public InvalidDirectoryNameException(SerializationInfo serializationInfo, StreamingContext context)
        : base(serializationInfo, context)
    {
    }

    public InvalidDirectoryNameException(string directoryName)
    {
        DirectoryName = directoryName;

        Code = FileExplorerErrorCodes.Directories.InvalidDirectoryName;

        WithData(nameof(DirectoryName), DirectoryName);
    }

    public virtual string DirectoryName { get; }
}