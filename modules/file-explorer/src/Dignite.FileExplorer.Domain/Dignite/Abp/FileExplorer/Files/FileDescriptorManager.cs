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
    public virtual async Task<FileDescriptor> CreateAsync<TContainer>(
                                        [NotNull] IRemoteStreamContent remoteStream,
                                        [NotNull] IEntity entity)
    {
        var containerName = BlobContainerNameAttribute.GetContainerName<TContainer>();
        return await CreateAsync(containerName, remoteStream, entity);
    }

    public virtual async Task<FileDescriptor> CreateAsync(
                                        [NotNull] string containerName,
                                        [NotNull] IRemoteStreamContent remoteStream,
                                        [NotNull] IEntity entity)
    {
        var blobName = (await GenerateBlobNameAsync(containerName));
        blobName += Path.GetExtension(remoteStream.FileName).EnsureStartsWith('.'); //Add the extended name
        var fileDescriptor = new FileDescriptor(
                    GuidGenerator.Create(),
                    containerName,
                    blobName,
                    remoteStream.ContentLength.GetValueOrDefault(),
                    remoteStream.FileName,
                    remoteStream.ContentType,
                    entity.GetType().FullName,
                    GetEntityId(entity),
                    CurrentTenant.Id);

        return await CreateAsync(fileDescriptor, remoteStream);
    }

    public virtual async Task<FileDescriptor> CreateAsync([NotNull] FileDescriptor fileInfo,
                                        [NotNull] IRemoteStreamContent fileStream)
    {
        return await base.CreateAsync(fileInfo, fileStream.GetStream());
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