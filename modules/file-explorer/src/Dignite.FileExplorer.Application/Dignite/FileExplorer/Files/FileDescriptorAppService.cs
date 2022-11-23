using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Dignite.Abp.BlobStoring;
using Dignite.FileExplorer.Permissions;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Authorization;
using Volo.Abp.BlobStoring;
using Volo.Abp.Content;

namespace Dignite.FileExplorer.Files;

public class FileDescriptorAppService : ApplicationService, IFileDescriptorAppService
{
    private readonly IFileDescriptorRepository _fileRepository;
    private readonly FileDescriptorManager _fileManager;
    private readonly IBlobContainerFactory _blobContainerFactory;
    private readonly IBlobContainerConfigurationProvider _blobContainerConfigurationProvider;

    public FileDescriptorAppService(
        IFileDescriptorRepository blobRepository,
        FileDescriptorManager fileManager,
        IBlobContainerFactory blobContainerFactory,
        IBlobContainerConfigurationProvider blobContainerConfigurationProvider)
    {
        _fileRepository = blobRepository;
        _fileManager = fileManager;
        _blobContainerFactory = blobContainerFactory;
        _blobContainerConfigurationProvider = blobContainerConfigurationProvider;
    }

    [Authorize]
    public async Task<FileDescriptorDto> CreateAsync(CreateFileInput input)
    {
        var fileDescriptor = await _fileManager.CreateAsync(input.ContainerName, input.File, input.DirectoryId, input.EntityId);

        try
        {
            await AuthorizationService.CheckAsync(fileDescriptor, CommonOperations.Create);
        }
        catch (AbpAuthorizationException ex)
        {
            await _fileManager.DeleteAsync(fileDescriptor);
        }
        return ObjectMapper.Map<FileDescriptor, FileDescriptorDto>(fileDescriptor);
    }

    [Authorize]
    public async Task<FileDescriptorDto> UpdateAsync(Guid id, UpdateFileInput input)
    {
        var entity = await _fileRepository.GetAsync(id);
        entity.Name = input.Name;
        await AuthorizationService.CheckAsync(entity, CommonOperations.Update);
        await _fileRepository.UpdateAsync(entity);
        return ObjectMapper.Map<FileDescriptor, FileDescriptorDto>(entity);
    }

    [Authorize]
    public async Task DeleteAsync(Guid id)
    {
        var result = await _fileRepository.FindAsync(id, false);
        if (result != null)
        {
            await AuthorizationService.CheckAsync(result, CommonOperations.Delete);
            await _fileManager.DeleteAsync(result);
        }
    }

    public async Task<FileDescriptorDto> GetAsync(Guid id)
    {
        var entity = await _fileRepository.GetAsync(id);
        await AuthorizationService.CheckAsync(entity, CommonOperations.Get);
        return ObjectMapper.Map<FileDescriptor, FileDescriptorDto>(entity);
    }

    /// <summary>
    ///
    /// </summary>
    /// <remarks>
    /// When the current user does not have <see cref="FileExplorerPermissions.Files.Management"/> permission, he can only get his own file
    /// </remarks>
    /// <param name="input"></param>
    /// <returns></returns>
    [Authorize]
    public async Task<PagedResultDto<FileDescriptorDto>> GetListAsync(GetFilesInput input)
    {
        if (!await AuthorizationService.IsGrantedAsync(FileExplorerPermissions.Files.Management))
        {
            input.CreatorId = CurrentUser.Id;
        }
        var count = await _fileRepository.GetCountAsync(input.ContainerName, input.CreatorId, input.DirectoryId, input.Filter, input.EntityId);
        var result = await _fileRepository.GetListAsync(input.ContainerName, input.CreatorId, input.DirectoryId, input.Filter, input.EntityId, input.Sorting, input.MaxResultCount, input.SkipCount);

        return new PagedResultDto<FileDescriptorDto>(
            count,
            ObjectMapper.Map<List<FileDescriptor>, List<FileDescriptorDto>>(result)
            );
    }

    public virtual async Task<IRemoteStreamContent> GetStreamAsync([NotNull] string containerName, [NotNull] string blobName)
    {
        var entity = await _fileManager.GetOrNullAsync(containerName, blobName);

        if (entity != null)
        {
            await AuthorizationService.CheckAsync(entity, CommonOperations.Get);
            var blobContainer = _blobContainerFactory.Create(containerName);
            Stream stream = await blobContainer.GetOrNullAsync(blobName);

            if (stream != null)
            {
                return new RemoteStreamContent(stream, entity?.Name, entity?.MimeType, entity?.Size, true);
            }
        }
        return null;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="containerName"></param>
    /// <returns></returns>
    public virtual Task<BlobHandlerConfigurationDto> GetBlobHandlerConfiguration([NotNull] string containerName)
    {
        var dto = new BlobHandlerConfigurationDto();
        var configuration = _blobContainerConfigurationProvider.Get(containerName);
        var blobSizeLimitConfiguration = configuration.GetBlobSizeLimitConfiguration();
        var fileTypeCheckConfiguration = configuration.GetFileTypeCheckConfiguration();
        dto.MaximumBlobSize= blobSizeLimitConfiguration.MaximumBlobSize;
        dto.AllowedFileTypeNames = fileTypeCheckConfiguration.AllowedFileTypeNames;

        return Task.FromResult(dto);
    }
}