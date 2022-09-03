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
using Volo.Abp.Guids;
using Volo.Abp.MultiTenancy;

namespace Dignite.FileExplorer.Files;

public class FileManager : FileManager<FileDescriptor,FileDescriptorStore>,IDomainService
{
    protected IGuidGenerator GuidGenerator => LazyServiceProvider.LazyGetService<IGuidGenerator>(SimpleGuidGenerator.Instance);
    protected ICurrentTenant CurrentTenant => LazyServiceProvider.LazyGetRequiredService<ICurrentTenant>();

    public async Task<FileDescriptor> CreateAsync<TContainer>(
                                        [NotNull] IRemoteStreamContent remoteStream,
                                        [NotNull] IEntity entity)
    {
        var containerName = BlobContainerNameAttribute.GetContainerName<TContainer>();
        return await CreateAsync(containerName, remoteStream, entity);
    }

    public async Task<FileDescriptor> CreateAsync(
                                        [NotNull] string containerName,
                                        [NotNull] IRemoteStreamContent remoteStream,
                                        [NotNull] IEntity entity)
    {         
        var blobName = (await GeneratorBlobNameAsync(containerName));
        blobName += Path.GetExtension(remoteStream.FileName).EnsureStartsWith('.'); //Add the extended name
        var fileInfo = new FileDescriptor(
                    GuidGenerator.Create(),
                    entity.GetType().FullName,
                    GetEntityId(entity),
                    new BasicFile(containerName, blobName),
                    remoteStream.FileName,
                    remoteStream.ContentType,
                    CurrentTenant.Id);
        
        return await CreateAsync(fileInfo,remoteStream);
    }

    public async Task<FileDescriptor> CreateAsync([NotNull] FileDescriptor fileInfo, 
                                        [NotNull] IRemoteStreamContent fileStream)
    {
        await CheckBlobInfoAsync(fileInfo);
        
        return await base.CreateAsync(fileInfo, fileStream.GetStream());        
    }

    private Task CheckBlobInfoAsync(FileDescriptor blobInfo)
    {
        Check.NotNullOrWhiteSpace(blobInfo.ContainerName, nameof(FileDescriptor.ContainerName), FileDescriptorConsts.MaxContainerNameLength);
        Check.NotNullOrWhiteSpace(blobInfo.BlobName, nameof(FileDescriptor.BlobName), FileDescriptorConsts.MaxBlobNameLength);
        Check.NotNullOrWhiteSpace(blobInfo.EntityTypeFullName, nameof(FileDescriptor.EntityTypeFullName), FileDescriptorConsts.MaxEntityTypeFullNameLength);
        Check.NotNullOrWhiteSpace(blobInfo.EntityId, nameof(FileDescriptor.EntityId), FileDescriptorConsts.MaxEntityIdLength);
        Check.Length(blobInfo.Name, nameof(FileDescriptor.Name), FileDescriptorConsts.MaxNameLength);


        return Task.CompletedTask;
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
}
