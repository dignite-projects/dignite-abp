using Dignite.FileExplorer.Files;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Dignite.FileExplorer.EntityFrameworkCore;

[ConnectionStringName(FileExplorerDbProperties.ConnectionStringName)]
public interface IFileExplorerDbContext : IEfCoreDbContext
{
    DbSet<FileDescriptor> FileDescriptors { get; }
}