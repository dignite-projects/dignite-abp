using System.Threading.Tasks;

namespace Dignite.Abp.BlobStoring;

public interface IBlobHandler
{
    Task ExecuteAsync(BlobHandlerContext context);
}
