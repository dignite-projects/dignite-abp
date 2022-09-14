using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace Dignite.FileExplorer.MongoDB;

[ConnectionStringName(FileExplorerDbProperties.ConnectionStringName)]
public interface IFileExplorerMongoDbContext : IAbpMongoDbContext
{
    /* Define mongo collections here. Example:
     * IMongoCollection<Question> Questions { get; }
     */
}