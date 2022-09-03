using System.Threading.Tasks;

namespace Dignite.Abp.FileStoring;

public interface IBlobHandler
{
    Task ExecuteAsync(BlobHandlerContext context);
}
