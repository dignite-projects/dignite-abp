using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
    /// <param name="remoteStreams"></param>
    /// <param name="directoryId">Directory in container</param>
    /// <param name="entity">Associated Entity</param>
    /// <returns></returns>
    public virtual async Task<IReadOnlyList<FileDescriptor>> CreateAsync<TContainer>(
                                        [NotNull] IReadOnlyList<IRemoteStreamContent> remoteStreams,
                                        [CanBeNull] Guid? directoryId,
                                        [CanBeNull] IEntity entity)
        where TContainer : class
    {
        var containerName = BlobContainerNameAttribute.GetContainerName<TContainer>();
        return await CreateAsync(containerName, remoteStreams,directoryId, entity);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="containerName"></param>
    /// <param name="remoteStreams"></param>
    /// <param name="directoryId">Directory in container</param>
    /// <param name="entity">Associated Entity</param>
    /// <returns></returns>
    public virtual async Task<IReadOnlyList<FileDescriptor>> CreateAsync(
                                        [NotNull] string containerName,
                                        [NotNull] IReadOnlyList<IRemoteStreamContent> remoteStreams,
                                        [CanBeNull] Guid? directoryId,
                                        [NotNull] IEntity entity)
    {
        var files = new List<FileDescriptor>();
        foreach (var stream in remoteStreams)
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
                        entity.GetType().FullName,
                        GetEntityKey(entity),
                        CurrentTenant.Id);

            files.Add(await CreateAsync(fileDescriptor, stream));
        }
        return files;
    }

    public virtual async Task<FileDescriptor> CreateAsync([NotNull] FileDescriptor fileInfo,
                                        [NotNull] IRemoteStreamContent remoteStream)
    {
        return await base.CreateAsync(fileInfo, remoteStream.GetStream());
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
        Check.NotNullOrWhiteSpace(file.EntityTypeFullName, nameof(FileDescriptor.EntityTypeFullName), FileDescriptorConsts.MaxEntityTypeFullNameLength);
        Check.NotNullOrWhiteSpace(file.EntityId, nameof(FileDescriptor.EntityId), FileDescriptorConsts.MaxEntityIdLength);
        return base.CheckFileAsync(file);
    }
}