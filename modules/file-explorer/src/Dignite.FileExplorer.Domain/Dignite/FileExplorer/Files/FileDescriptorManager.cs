using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Dignite.Abp.BlobStoring;
using Dignite.Abp.Files;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.BlobStoring;
using Volo.Abp.Content;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Services;

namespace Dignite.FileExplorer.Files;

public class FileDescriptorManager : FileManager<FileDescriptor, FileDescriptorStore>, IDomainService
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TContainer"></typeparam>
    /// <param name="stream"></param>
    /// <param name="directoryId">Directory in container</param>
    /// <param name="entity">Associated Entity</param>
    /// <returns></returns>
    public virtual async Task<FileDescriptor> CreateAsync<TContainer>(
                                        [NotNull] IRemoteStreamContent stream,
                                        [CanBeNull] Guid? directoryId,
                                        [CanBeNull] IEntity entity)
        where TContainer : class
    {
        var containerName = BlobContainerNameAttribute.GetContainerName<TContainer>();
        return await CreateAsync(containerName, stream, directoryId, entity);
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="containerName"></param>
    /// <param name="stream"></param>
    /// <param name="directoryId">Directory in container</param>
    /// <param name="entity">Associated Entity</param>
    /// <returns></returns>
    public virtual async Task<FileDescriptor> CreateAsync(
                                        [NotNull] string containerName,
                                        [NotNull] IRemoteStreamContent stream,
                                        [CanBeNull] Guid? directoryId,
                                        [CanBeNull] IEntity entity)
    {
        return await CreateAsync(
            containerName,
            stream,
            directoryId,
            entity == null ? null : entity.GetType().FullName,
            entity == null ? null : GetEntityKey(entity)
            );
    }

    public virtual async Task<FileDescriptor> CreateAsync(
                                        [NotNull] string containerName,
                                        [NotNull] IRemoteStreamContent stream,
                                        [CanBeNull] Guid? directoryId,
                                        [CanBeNull] string entityType,
                                        [CanBeNull] string entityId)
    {
        var blobName = (await GenerateBlobNameAsync(containerName));
        blobName += Path.GetExtension(stream.FileName).EnsureStartsWith('.'); //Add the extended name
        var fileDescriptor = new FileDescriptor(
                    GuidGenerator.Create(),
                    containerName,
                    blobName,
                    directoryId,
                    stream.ContentLength.GetValueOrDefault(),
                    stream.FileName,
                    stream.ContentType,
                    entityType,
                    entityId,
                    CurrentTenant.Id);

        return await CreateAsync(fileDescriptor, stream);
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="file"></param>
    /// <param name="remoteStream"></param>
    /// <returns></returns>
    public virtual async Task<FileDescriptor> CreateAsync([NotNull] FileDescriptor file,
                                        [NotNull] IRemoteStreamContent remoteStream)
    {
        return await base.CreateAsync(file, remoteStream.GetStream());
    }

    protected virtual string GetEntityKey(IEntity entity)
    {
        var keys = entity.GetKeys();
        if (keys.All(k => k == null))
        {
            return null;
        }

        return keys.JoinAsString(",");
    }

    protected override Task CheckFileAsync([NotNull] FileDescriptor file)
    {
        Check.NotNullOrWhiteSpace(file.EntityType, nameof(FileDescriptor.EntityType), FileDescriptorConsts.MaxEntityTypeLength);
        Check.NotNullOrWhiteSpace(file.EntityId, nameof(FileDescriptor.EntityId), FileDescriptorConsts.MaxEntityIdLength);
        return base.CheckFileAsync(file);
    }

    /// <summary>
    /// Generate blobname using the configured <see cref="IBlobNameGenerator"/>
    /// </summary>
    /// <param name="containerName"></param>
    /// <returns></returns>
    private async Task<string> GenerateBlobNameAsync(string containerName)
    {
        var configuration = BlobContainerConfigurationProvider.Get(containerName);
        var namingGeneratorType = configuration.GetConfigurationOrDefault(
            BlobContainerConfigurationNames.BlobNamingGenerator,
            typeof(RandomBlobNameGenerator)
            );

        var generator = LazyServiceProvider.LazyGetRequiredService(namingGeneratorType)
            .As<IBlobNameGenerator>();

        var blobName = await generator.Create();
        return blobName;
    }
}