using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Volo.Abp.BlobStoring;
using Volo.Abp.Collections;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.BlobStoring.InfoPersistent;

public abstract class BlobManager<TBlobInfo,TBlobInfoStore> 
    where TBlobInfo : IBlobInfo
    where TBlobInfoStore:IBlobInfoStore<TBlobInfo>
{
    public IAbpLazyServiceProvider LazyServiceProvider { get; set; }
    protected IServiceProvider ServiceProvider => LazyServiceProvider.LazyGetRequiredService<IServiceProvider>();
    protected IBlobContainerConfigurationProvider BlobContainerConfigurationProvider=> LazyServiceProvider.LazyGetRequiredService<IBlobContainerConfigurationProvider>();

    protected IBlobContainerFactory BlobContainerFactory=> LazyServiceProvider.LazyGetRequiredService<IBlobContainerFactory>();
    protected ICurrentBlobInfo CurrentBlobInfo=> LazyServiceProvider.LazyGetRequiredService<ICurrentBlobInfo>();

    protected ILoggerFactory LoggerFactory => LazyServiceProvider.LazyGetRequiredService<ILoggerFactory>();

    protected ILogger Logger => LazyServiceProvider.LazyGetService<ILogger>(provider => LoggerFactory?.CreateLogger(GetType().FullName) ?? NullLogger.Instance);

    protected IBlobInfoStore<TBlobInfo> BlobInfoStore => LazyServiceProvider.LazyGetRequiredService(typeof(TBlobInfoStore)).As<IBlobInfoStore<TBlobInfo>>();

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
            await BlobProcessHandlers(blobInfo.ContainerName, stream);

            // 计算stream hash
            var hash = stream.ToMd5();
            if (await BlobInfoStore.HashExistsAsync(blobInfo.ContainerName, hash, cancellationToken))
            {
                // 如果存在相同hash的blob，则创建其副本
                var mainBlob = await BlobInfoStore.FindByHashAsync(blobInfo.ContainerName, hash, cancellationToken);
                blobInfo.ReferBlobName = mainBlob.BlobName;

                // 记录Blob信息
                await BlobInfoStore.CreateAsync(blobInfo, cancellationToken);
            }
            else
            {
                blobInfo.Size = stream.Length;
                blobInfo.Hash = hash;

                // 记录Blob信息；
                await BlobInfoStore.CreateAsync(blobInfo, cancellationToken);

                //
                var blobContainer = BlobContainerFactory.Create(blobInfo.ContainerName);
                await blobContainer.SaveAsync(blobInfo.BlobName, stream, overrideExisting, cancellationToken);
            }
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


    public virtual async Task<TBlobInfo> GetOrNullAsync([NotNull] string containerName, [NotNull] string name, CancellationToken cancellationToken = default)
    {
        var blobInfo = await BlobInfoStore.FindAsync(containerName, name, cancellationToken);
        return blobInfo;
    }

    public virtual async Task<bool> DeleteAsync([NotNull] TBlobInfo blobInfo, CancellationToken cancellationToken = default)
    {
        var blobContainer = BlobContainerFactory.Create(blobInfo.ContainerName);

        await BlobInfoStore.DeleteAsync(blobInfo, cancellationToken); //Delete blob info

        //
        if (blobInfo.ReferBlobName.IsNullOrEmpty())
        {
            if (!await BlobInfoStore.ReferenceExistsAsync(blobInfo.ContainerName, blobInfo.BlobName, cancellationToken))
            {
                return await blobContainer.DeleteAsync(blobInfo.BlobName, cancellationToken);
            }
        }
        else
        {
            if (!await BlobInfoStore.ReferenceExistsAsync(blobInfo.ContainerName, blobInfo.ReferBlobName, cancellationToken))
            {
                return await blobContainer.DeleteAsync(blobInfo.ReferBlobName, cancellationToken);
            }
        }

        return true;
    }


    private async Task BlobProcessHandlers(string containerName, Stream stream)
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
