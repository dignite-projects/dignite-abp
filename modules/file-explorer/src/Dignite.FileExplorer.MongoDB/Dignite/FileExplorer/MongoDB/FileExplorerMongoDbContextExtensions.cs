using Dignite.FileExplorer.Files;
using Volo.Abp;
using Volo.Abp.MongoDB;

namespace Dignite.FileExplorer.MongoDB;

public static class FileExplorerMongoDbContextExtensions
{
    public static void ConfigureFileExplorer(
        this IMongoModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        builder.Entity<FileDescriptor>(x =>
        {
            x.CollectionName = FileExplorerDbProperties.DbTablePrefix + "FileDescriptor";
        });
    }
}