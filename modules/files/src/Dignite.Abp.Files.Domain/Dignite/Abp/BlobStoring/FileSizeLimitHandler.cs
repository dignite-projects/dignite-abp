using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.BlobStoring;

/// <summary>
/// Handler for limiting the size of blob
/// </summary>
public class FileSizeLimitHandler : IFileHandler, ITransientDependency
{
    public Task ExecuteAsync(FileHandlerContext context)
    {
        var configuration = context.ContainerConfiguration.GetFileSizeLimitConfiguration();
        if (configuration.MaxFileSize * 1024 < context.BlobStream.Length)
        {
            throw new BusinessException(
                code: "Dignite.Abp.BlobStoring:010008",
                message: "Blob object is too large",
                details: $"The blob object size cannot exceed {configuration.MaxFileSize}M!"
            );
        }

        return Task.CompletedTask;
    }
}