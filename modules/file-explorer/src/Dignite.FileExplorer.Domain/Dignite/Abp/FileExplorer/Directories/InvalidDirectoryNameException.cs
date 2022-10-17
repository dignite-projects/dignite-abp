using System.Runtime.Serialization;
using Dignite.FileExplorer;
using Volo.Abp;

namespace Dignite.Abp.FileExplorer.Directories;
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
