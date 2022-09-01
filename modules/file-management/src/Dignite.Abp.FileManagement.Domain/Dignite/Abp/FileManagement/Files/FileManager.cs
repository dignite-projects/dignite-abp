using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Dignite.Abp.BlobStoring.InfoPersistent;
using JetBrains.Annotations;
using Microsoft.Extensions.Options;
using Volo.Abp;
using Volo.Abp.Content;
using Volo.Abp.Domain.Services;
using Volo.Abp.Guids;
using Volo.Abp.MultiTenancy;

namespace Dignite.Abp.FileManagement.Files;

public class FileManager : BlobManager<FileDescriptor,FileDescriptorStore>,IDomainService
{
    protected IGuidGenerator GuidGenerator => LazyServiceProvider.LazyGetService<IGuidGenerator>(SimpleGuidGenerator.Instance);
    protected ICurrentTenant CurrentTenant => LazyServiceProvider.LazyGetRequiredService<ICurrentTenant>();

    public async Task<FileDescriptor> CreateAsync([NotNull] string entityType,
                                        [NotNull] string entityId,
                                        [NotNull] string containerName,
                                        [NotNull] IRemoteStreamContent remoteStream)
    {         
        var blobName = (await GeneratorBlobNameAsync(containerName));
        blobName += Path.GetExtension(remoteStream.FileName).EnsureStartsWith('.'); //Add the extended name
        var fileInfo = new FileDescriptor(
                    GuidGenerator.Create(),
                    entityType,
                    entityId,
                    new BasicBlobInfo(containerName, blobName),
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
        Check.NotNullOrWhiteSpace(blobInfo.EntityTypeFullName, nameof(FileDescriptor.EntityTypeFullName), FileDescriptorConsts.MaxEntityTypeFullNameLength);
        Check.NotNullOrWhiteSpace(blobInfo.EntityId, nameof(FileDescriptor.EntityId), FileDescriptorConsts.MaxEntityIdLength);
        Check.Length(blobInfo.Name, nameof(FileDescriptor.Name), FileDescriptorConsts.MaxNameLength);
        Check.NotNullOrWhiteSpace(blobInfo.ContainerName, nameof(FileDescriptor.ContainerName), FileDescriptorConsts.MaxContainerNameLength);
        Check.NotNullOrWhiteSpace(blobInfo.BlobName, nameof(FileDescriptor.BlobName), FileDescriptorConsts.MaxBlobNameLength);


        return Task.CompletedTask;
    }
}
