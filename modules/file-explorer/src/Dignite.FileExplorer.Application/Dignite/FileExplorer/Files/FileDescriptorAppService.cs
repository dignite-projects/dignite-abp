﻿using System;
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
        // Build a temporary file for authorization verification
        var tempFileDescriptor = new FileDescriptor(Guid.NewGuid(), input.ContainerName, string.Empty, string.Empty, string.Empty, input.DirectoryId, input.EntityId, CurrentTenant.Id);
        tempFileDescriptor.CreatorId = CurrentUser.Id;
        await AuthorizationService.CheckAsync(tempFileDescriptor, CommonOperations.Create);

        // formal start of file creation
        var fileDescriptor = await _fileManager.CreateAsync(input.ContainerName, input.File, input.DirectoryId, input.EntityId);
        return ObjectMapper.Map<FileDescriptor, FileDescriptorDto>(fileDescriptor);
    }

    [Authorize]
    public async Task<FileDescriptorDto> UpdateAsync(Guid id, UpdateFileInput input)
    {
        var entity = await _fileRepository.GetAsync(id);
        entity.DirectoryId= input.DirectoryId;
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
            if (!entity.ReferBlobName.IsNullOrEmpty())
            { 
                blobName= entity.ReferBlobName;
            }

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
    public virtual Task<FileContainerConfigurationDto> GetFileContainerConfiguration([NotNull] string containerName)
    {
        var dto = new FileContainerConfigurationDto();
        var configuration = _blobContainerConfigurationProvider.Get(containerName);
        var blobSizeLimitConfiguration = configuration.GetFileSizeLimitConfiguration();
        var fileTypeCheckConfiguration = configuration.GetFileTypeCheckConfiguration();
        var authorizationConfiguration = configuration.GetAuthorizationConfiguration();
        dto.MaxBlobSize= blobSizeLimitConfiguration.MaxFileSize;
        dto.AllowedFileTypeNames = fileTypeCheckConfiguration.AllowedFileTypeNames;
        dto.CreateDirectoryPermissionName= authorizationConfiguration.CreateDirectoryPermissionName;
        dto.CreateFilePermissionName= authorizationConfiguration.CreateFilePermissionName;
        dto.UpdateFilePermissionName= authorizationConfiguration.UpdateFilePermissionName;
        dto.DeleteFilePermissionName= authorizationConfiguration.DeleteFilePermissionName;
        dto.GetFilePermissionName= authorizationConfiguration.GetFilePermissionName;

        return Task.FromResult(dto);
    }
}