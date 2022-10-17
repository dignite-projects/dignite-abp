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
    public virtual async Task<IReadOnlyList<FileDescriptor>> CreateAsync<TContainer>(
                                        [NotNull] IEntity entity,
                                        [NotNull] IReadOnlyList<IRemoteStreamContent> remoteStreams)
        where TContainer : class
    {
        var containerName = BlobContainerNameAttribute.GetContainerName<TContainer>();
        return await CreateAsync(entity,containerName, remoteStreams);
    }

    public virtual async Task<IReadOnlyList<FileDescriptor>> CreateAsync(
                                        [NotNull] IEntity entity,
                                        [NotNull] string containerName,
                                        [NotNull] IReadOnlyList<IRemoteStreamContent> remoteStreams)
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
                        stream.ContentLength.GetValueOrDefault(),
                        stream.FileName,
                        stream.ContentType,
                        entity.GetType().FullName,
                        GetEntityId(entity),
                        CurrentTenant.Id);

            files.Add( await CreateAsync(fileDescriptor, stream));
        }
        return files;
    }

    public virtual async Task<FileDescriptor> CreateAsync([NotNull] FileDescriptor fileInfo,
                                        [NotNull] IRemoteStreamContent remoteStream)
    {
        return await base.CreateAsync(fileInfo, remoteStream.GetStream());
    }

    protected virtual string GetEntityId(IEntity entity)
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