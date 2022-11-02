using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dignite.Abp.BlobStoring;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using Volo.Abp.BlobStoring;
using Volo.Abp.Collections;
using Volo.Abp.Domain.Services;

namespace Dignite.Abp.Files;

public abstract class FileManager<TFile, TFileStore> : DomainService
    where TFile : IFile
    where TFileStore : IFileStore<TFile>
{
    protected IBlobContainerConfigurationProvider BlobContainerConfigurationProvider => LazyServiceProvider.LazyGetRequiredService<IBlobContainerConfigurationProvider>();
    protected IBlobContainerFactory BlobContainerFactory => LazyServiceProvider.LazyGetRequiredService<IBlobContainerFactory>();
    protected IFileStore<TFile> FileStore => LazyServiceProvider.LazyGetService(typeof(TFileStore)).As<IFileStore<TFile>>();
    protected ContainerNameValidator ContainerNameValidator => LazyServiceProvider.LazyGetRequiredService<ContainerNameValidator>();

    /// <summary>
    ///
    /// </summary>
    /// <param name="file"></param>
    /// <param name="stream"></param>
    /// <param name="overrideExisting"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual async Task<TFile> CreateAsync(
            [NotNull] TFile file,
            [NotNull] Stream stream,
            bool overrideExisting = false,
            CancellationToken cancellationToken = default)
    {
        ContainerNameValidator.Validate(file.ContainerName);
        await CheckFileAsync(file);

        await OnCreatingEntityAsync(file);

        //Give it to the handlers before saving
        await FileHandlers(file, stream);

        //Persist file information
        await FileStore.CreateAsync(file, false, cancellationToken);

        //Save file stream to container
        var blobContainer = BlobContainerFactory.Create(file.ContainerName);
        await blobContainer.SaveAsync(file.BlobName, stream, overrideExisting, cancellationToken);

        await OnCreatedEntityAsync(file);

        return file;
    }

    protected virtual Task OnCreatingEntityAsync([NotNull] TFile file)
    {
        return Task.CompletedTask;
    }

    protected virtual Task OnCreatedEntityAsync([NotNull] TFile file)
    {
        return Task.CompletedTask;
    }

    public virtual async Task<TFile> GetOrNullAsync([NotNull] string containerName, [NotNull] string blobName, CancellationToken cancellationToken = default)
    {
        var blobInfo = await FileStore.FindAsync(containerName, blobName, cancellationToken);
        return blobInfo;
    }

    public virtual async Task<TFile> GetOrNullAsync<TContainer>([NotNull] string blobName, CancellationToken cancellationToken = default)
        where TContainer : class
    {
        var containerName = BlobContainerNameAttribute.GetContainerName<TContainer>();
        return await GetOrNullAsync(containerName, blobName, cancellationToken);
    }

    public virtual async Task<Stream> GetStreamOrNullAsync([NotNull] string containerName, [NotNull] string blobName, CancellationToken cancellationToken = default)
    {
        var blobContainer = BlobContainerFactory.Create(containerName);

        return await blobContainer.GetOrNullAsync(blobName, cancellationToken);
    }

    public virtual async Task<Stream> GetStreamOrNullAsync<TContainer>([NotNull] string blobName, CancellationToken cancellationToken = default)
        where TContainer : class
    {
        var blobContainer = BlobContainerFactory.Create<TContainer>();

        return await blobContainer.GetOrNullAsync(blobName, cancellationToken);
    }

    public virtual async Task<bool> DeleteAsync([NotNull] TFile file, CancellationToken cancellationToken = default)
    {
        await OnDeletingEntityAsync(file);

        await FileStore.DeleteAsync(file, false, cancellationToken);

        //delete blob
        if (!await FileStore.BlobNameExistsAsync(file.ContainerName, file.BlobName, file.Id, cancellationToken))
        {
            var blobContainer = BlobContainerFactory.Create(file.ContainerName);
            return await blobContainer.DeleteAsync(file.BlobName, cancellationToken);
        }

        await OnDeletedEntityAsync(file);

        return true;
    }

    protected virtual Task OnDeletingEntityAsync([NotNull] TFile file)
    {
        return Task.CompletedTask;
    }

    protected virtual Task OnDeletedEntityAsync([NotNull] TFile file)
    {
        return Task.CompletedTask;
    }

    /// <summary>
    /// file handlers
    /// </summary>
    /// <param name="file"></param>
    /// <param name="stream"></param>
    /// <returns></returns>
    private async Task FileHandlers(TFile file, Stream stream)
    {
        var configuration = BlobContainerConfigurationProvider.Get(file.ContainerName);
        // blob process handlers
        var processHandlers = configuration.GetConfigurationOrDefault<ITypeList<IBlobHandler>>(BlobContainerConfigurationNames.BlobHandlers, null);
        if (processHandlers != null && processHandlers.Any())
        {
            var serviceProvider = LazyServiceProvider.LazyGetRequiredService<IServiceProvider>();
            var context = new BlobHandlerContext(file, stream, configuration);
            using (var scope = serviceProvider.CreateScope())
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

    protected virtual Task CheckFileAsync([NotNull] TFile file)
    {
        Check.NotNullOrWhiteSpace(file.ContainerName, nameof(IFile.ContainerName), AbpFileConsts.MaxContainerNameLength);
        Check.NotNullOrWhiteSpace(file.BlobName, nameof(IFile.BlobName), AbpFileConsts.MaxBlobNameLength);
        Check.Length(file.Name, nameof(IFile.Name), AbpFileConsts.MaxNameLength);
        Check.Length(file.Name, nameof(IFile.MimeType), AbpFileConsts.MaxMimeTypeLength);

        return Task.CompletedTask;
    }
}