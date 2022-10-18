using Dignite.FileExplorer.Directories;
using Dignite.FileExplorer.Files;
using MongoDB.Driver;
using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace Dignite.FileExplorer.MongoDB;

[ConnectionStringName(FileExplorerDbProperties.ConnectionStringName)]
public class FileExplorerMongoDbContext : AbpMongoDbContext, IFileExplorerMongoDbContext
{
    public IMongoCollection<DirectoryDescriptor> DirectoryDescriptors => Collection<DirectoryDescriptor>();
    public IMongoCollection<FileDescriptor> FileDescriptors => Collection<FileDescriptor>();

    protected override void CreateModel(IMongoModelBuilder modelBuilder)
    {
        base.CreateModel(modelBuilder);

        modelBuilder.ConfigureFileExplorer();
    }
}