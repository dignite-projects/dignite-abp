using Dignite.FileExplorer.Directories;
using Dignite.FileExplorer.Files;
using MongoDB.Driver;
using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace Dignite.FileExplorer.MongoDB;

[ConnectionStringName(FileExplorerDbProperties.ConnectionStringName)]
public interface IFileExplorerMongoDbContext : IAbpMongoDbContext
{
    IMongoCollection<DirectoryDescriptor> DirectoryDescriptors { get; }
    IMongoCollection<FileDescriptor> FileDescriptors { get; }
}