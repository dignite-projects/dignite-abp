using Dignite.FileExplorer.Files;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Dignite.FileExplorer.EntityFrameworkCore;

[ConnectionStringName(FileExplorerDbProperties.ConnectionStringName)]
public class FileExplorerDbContext : AbpDbContext<FileExplorerDbContext>, IFileExplorerDbContext
{
    public DbSet<FileDescriptor> FileDescriptors { get; set; }

    public FileExplorerDbContext(DbContextOptions<FileExplorerDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureFileExplorer();
    }
}
