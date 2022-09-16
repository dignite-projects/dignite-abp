using Dignite.FileExplorer.Files;
using MongoDB.Driver;
using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace Dignite.FileExplorer.MongoDB;

[ConnectionStringName(FileExplorerDbProperties.ConnectionStringName)]
public interface IFileExplorerMongoDbContext : IAbpMongoDbContext
{
    IMongoCollection<FileDescriptor> FileDescriptors { get; }
}