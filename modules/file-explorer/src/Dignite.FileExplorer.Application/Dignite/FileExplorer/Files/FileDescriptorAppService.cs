using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Dignite.Abp.BlobStoring;
using Dignite.FileExplorer.Permissions;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.BlobStoring;
using Volo.Abp.Content;
using Volo.Abp.Imaging;

namespace Dignite.FileExplorer.Files;

public class FileDescriptorAppService : ApplicationService, IFileDescriptorAppService
{
    private readonly IFileDescriptorRepository _fileRepository;
    private readonly FileDescriptorManager _fileManager;
    private readonly IBlobContainerFactory _blobContainerFactory;
    private readonly IBlobContainerConfigurationProvider _blobContainerConfigurationProvider;
    private readonly IImageResizer _imageResizer;

    public FileDescriptorAppService(
        IFileDescriptorRepository blobRepository,
        FileDescriptorManager fileManager,
        IBlobContainerFactory blobContainerFactory,
        IBlobContainerConfigurationProvider blobContainerConfigurationProvider,
        IImageResizer imageResizer)
    {
        _fileRepository = blobRepository;
        _fileManager = fileManager;
        _blobContainerFactory = blobContainerFactory;
        _blobContainerConfigurationProvider = blobContainerConfigurationProvider;
        _imageResizer = imageResizer;
    }

    [Authorize]
    public async Task<FileDescriptorDto> CreateAsync(CreateFileInput input)
    {
        // Build a temporary file for authorization verification
        var tempFileDescriptor = new FileDescriptor(Guid.NewGuid(), input.ContainerName, string.Empty, string.Empty, string.Empty, input.CellName, input.DirectoryId, input.EntityId, CurrentTenant.Id);
        tempFileDescriptor.CreatorId = CurrentUser.Id;
        await AuthorizationService.CheckAsync(tempFileDescriptor, CommonOperations.Create);

        // formal start of file creation
        var fileDescriptor = await _fileManager.CreateAsync(input.ContainerName, input.File, input.CellName, input.DirectoryId, input.EntityId);
        return ObjectMapper.Map<FileDescriptor, FileDescriptorDto>(fileDescriptor);
    }

    [Authorize]
    public async Task<FileDescriptorDto> UpdateAsync(Guid id, UpdateFileInput input)
    {
        var entity = await _fileRepository.GetAsync(id);
        entity.DirectoryId= input.DirectoryId;
        entity.Name = input.Name;
        entity.CellName = input.CellName;
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

    public virtual async Task<IRemoteStreamContent> GetStreamAsync([NotNull] string containerName, [NotNull] string blobName, ImageResizeInput imageResize = null)
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
                if (imageResize!=null && (imageResize.Width>0 || imageResize.Height>0))
                {
                    if (ImageFormatHelper.IsValidImage(stream, ImageFormatHelper.AllowedImageUploadFormats))
                    {
                        var result = await _imageResizer.ResizeAsync(
                        stream,
                        new ImageResizeArgs(imageResize.Width, imageResize.Height, ImageResizeMode.Crop),
                        entity?.MimeType
                        );

                        if (result.State == ImageProcessState.Done)
                        {
                            stream = result.Result;
                        }
                    }
                }

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
    public virtual Task<FileContainerConfigurationDto> GetFileContainerConfigurationAsync([NotNull] string containerName)
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
        dto.FileCells = configuration
            .GetFileGridConfiguration()
            .FileCells
            .Select(c=>
                new FileCellDto(
                    c.Name,
                    c.DisplayName?.Localize(StringLocalizerFactory)
                    )
                ).ToList();

        return Task.FromResult(dto);
    }
}