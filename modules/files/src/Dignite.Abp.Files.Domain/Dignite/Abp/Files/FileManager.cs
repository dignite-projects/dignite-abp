using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dignite.Abp.FileStoring;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Volo.Abp.BlobStoring;
using Volo.Abp.Collections;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.Files;

public abstract class FileManager<TBlobInfo,TBlobInfoStore> 
    where TBlobInfo : IFile
    where TBlobInfoStore:IFileStore<TBlobInfo>
{
    public IAbpLazyServiceProvider LazyServiceProvider { get; set; }
    protected IServiceProvider ServiceProvider => LazyServiceProvider.LazyGetRequiredService<IServiceProvider>();
    protected IBlobContainerConfigurationProvider BlobContainerConfigurationProvider=> LazyServiceProvider.LazyGetRequiredService<IBlobContainerConfigurationProvider>();

    protected IBlobContainerFactory BlobContainerFactory=> LazyServiceProvider.LazyGetRequiredService<IBlobContainerFactory>();
    protected ICurrentFile CurrentBlobInfo=> LazyServiceProvider.LazyGetRequiredService<ICurrentFile>();

    protected ILoggerFactory LoggerFactory => LazyServiceProvider.LazyGetRequiredService<ILoggerFactory>();

    protected ILogger Logger => LazyServiceProvider.LazyGetService<ILogger>(provider => LoggerFactory?.CreateLogger(GetType().FullName) ?? NullLogger.Instance);

    protected IFileStore<TBlobInfo> BlobInfoStore => LazyServiceProvider.LazyGetRequiredService(typeof(TBlobInfoStore)).As<IFileStore<TBlobInfo>>();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="blobInfo"></param>
    /// <param name="stream"></param>
    /// <param name="overrideExisting"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual async Task<TBlobInfo> CreateAsync(
        [NotNull] TBlobInfo blobInfo, 
        [NotNull] Stream stream,
        bool overrideExisting = false, 
        CancellationToken cancellationToken = default)
    {
        using (CurrentBlobInfo.Current(blobInfo))
        {
            //
            await BlobHandlers(blobInfo.ContainerName, stream);
            blobInfo.Size = stream.Length;

            await BlobInfoStore.CreateAsync(blobInfo, false, cancellationToken);

            var blobContainer = BlobContainerFactory.Create(blobInfo.ContainerName);
            await blobContainer.SaveAsync(blobInfo.BlobName, stream, overrideExisting, cancellationToken);
        }

        return blobInfo;
    }

    /// <summary>
    /// Generate blobname using the configured <see cref="IBlobNameGenerator"/>
    /// </summary>
    /// <param name="containerName"></param>
    /// <returns></returns>
    public virtual async Task<string> GeneratorBlobNameAsync(string containerName)
    {
        var configuration = BlobContainerConfigurationProvider.Get(containerName);
        var namingGeneratorType = configuration.GetConfigurationOrDefault(
            BlobContainerConfigurationNames.BlobNamingGenerator,
            typeof(SimpleBlobNameGenerator)
            );

        var generator = LazyServiceProvider.LazyGetRequiredService(namingGeneratorType)
            .As<IBlobNameGenerator>();

        var blobName = await generator.Create();
        return blobName;
    }


    public virtual async Task<TBlobInfo> GetOrNullAsync([NotNull] string containerName, [NotNull] string blobName, CancellationToken cancellationToken = default)
    {
        var blobInfo = await BlobInfoStore.FindAsync(containerName, blobName, cancellationToken);
        return blobInfo;
    }

    public virtual async Task<bool> DeleteAsync([NotNull] TBlobInfo blobInfo, CancellationToken cancellationToken = default)
    {
        await BlobInfoStore.DeleteAsync(blobInfo,true, cancellationToken); //Delete blob info

        if (!await BlobInfoStore.ExistsAsync(blobInfo.ContainerName, blobInfo.BlobName, cancellationToken))
        {
            var blobContainer = BlobContainerFactory.Create(blobInfo.ContainerName);
            return await blobContainer.DeleteAsync(blobInfo.BlobName, cancellationToken);
        }

        return true;
    }


    protected virtual async Task BlobHandlers(string containerName, Stream stream)
    {
        var configuration = BlobContainerConfigurationProvider.Get(containerName);
        // blob process handlers
        var processHandlers = configuration.GetConfigurationOrDefault<ITypeList<IBlobHandler>>(BlobContainerConfigurationNames.BlobHandlers, null);
        if (processHandlers != null && processHandlers.Any())
        {
            var context = new BlobHandlerContext(stream, configuration, ServiceProvider);
            using (var scope = ServiceProvider.CreateScope())
            {
                foreach (var handlerType in processHandlers)
                {
                    var handler = scope.ServiceProvider
                        .GetRequiredService(handlerType)
                        .As<IBlobHandler>();

                    await handler.ExecuteAsync(context);
                }
            }
        }
    }
}
