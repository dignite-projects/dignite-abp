using System.Threading.Tasks;

namespace Dignite.Abp.BlobStoring;

public interface IFileHandler
{
    Task ExecuteAsync(FileHandlerContext context);
}