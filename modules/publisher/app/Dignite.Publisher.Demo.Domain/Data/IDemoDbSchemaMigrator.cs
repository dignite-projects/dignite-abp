using System.Threading.Tasks;

namespace Dignite.Publisher.Demo.Data;

public interface IDemoDbSchemaMigrator
{
    Task MigrateAsync();
}
