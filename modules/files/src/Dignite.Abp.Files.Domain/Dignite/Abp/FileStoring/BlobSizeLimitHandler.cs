using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.FileStoring;

/// <summary>
/// Handler for limiting the size of blob
/// </summary>
public class BlobSizeLimitHandler : IBlobHandler, ITransientDependency
{
    public Task ExecuteAsync(BlobHandlerContext context)
    {
        var configuration = context.ContainerConfiguration.GetBlobSizeLimitConfiguration();
        if (configuration.MaximumBlobSize * 1024 < context.BlobStream.Length)
        {
            throw new BusinessException(
                code: "Dignite.Abp.FileStoring:010008",
                message: "Blob object is too large",
                details: $"The blob object size cannot exceed {configuration.MaximumBlobSize}M!"
            );
        }

        return Task.CompletedTask;
    }
}
