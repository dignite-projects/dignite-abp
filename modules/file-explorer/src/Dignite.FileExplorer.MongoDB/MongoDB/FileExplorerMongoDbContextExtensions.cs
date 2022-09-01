using Volo.Abp;
using Volo.Abp.MongoDB;

namespace Dignite.FileExplorer.MongoDB;

public static class FileExplorerMongoDbContextExtensions
{
    public static void ConfigureFileExplorer(
        this IMongoModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));
    }
}
