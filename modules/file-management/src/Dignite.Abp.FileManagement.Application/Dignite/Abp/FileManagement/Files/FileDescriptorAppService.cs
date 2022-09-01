using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;
using Dignite.Abp.BlobStoring;
using JetBrains.Annotations;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.BlobStoring;
using Volo.Abp.Content;
using Volo.Abp.Validation;

namespace Dignite.Abp.FileManagement.Files;

public class FileDescriptorAppService : ApplicationService, IFileDescriptorAppService
{
    private readonly IFileDescriptorRepository _blobRepository;
    private readonly FileManager _fileManager;
    private readonly IBlobContainerConfigurationProvider _blobContainerConfigurationProvider;
    private readonly IBlobContainerFactory _blobContainerFactory;

    public FileDescriptorAppService(
        IFileDescriptorRepository blobRepository, 
        FileManager fileManager, 
        IBlobContainerConfigurationProvider blobContainerConfigurationProvider, 
        IBlobContainerFactory blobContainerFactory)
    {
        _blobRepository = blobRepository;
        _fileManager = fileManager;
        _blobContainerConfigurationProvider = blobContainerConfigurationProvider;
        _blobContainerFactory = blobContainerFactory;
    }




    /// <summary>
    /// 
    /// </summary>
    /// <param name="input"></param>
    /// <returns>
    /// Return file descriptor after successful saving
    /// </returns>
    public async Task<FileDescriptorDto> CreateAsync(CreateFileInput input)
    {
        if (input.File == null)
        {
            ThrowValidationException("Bytes of file can not be null or empty!", nameof(input.File));
        }

        var file = await _fileManager.CreateAsync(input.EntityTypeFullName, input.EntityId, input.ContainerName, input.File);

        return ObjectMapper.Map<FileDescriptor, FileDescriptorDto>(file);
    }

    public virtual async Task<IRemoteStreamContent> GetFileAsync([NotNull] string containerName, [NotNull] string blobName)
    {
        var file = await _fileManager.GetOrNullAsync(containerName, blobName);

        var blobContainer = _blobContainerFactory.Create(containerName);
        Stream stream;
        //
        if (file != null)
        {
            if (!file.ReferBlobName.IsNullOrEmpty())
                stream = await blobContainer.GetOrNullAsync(file.ReferBlobName);
            else
                stream = await blobContainer.GetOrNullAsync(blobName);
        }
        else
        {
            stream = await blobContainer.GetOrNullAsync(blobName);
        }

        if (stream != null)
        {
            return new RemoteStreamContent(stream, file?.Name, file?.MimeType, file?.Size, true);
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<PagedResultDto<FileDescriptorDto>> GetListAsync(GetFilesInput input)
    {
        var count = await _blobRepository.GetCountAsync(input.ContainerName, input.Filter, input.EntityTypeFullName, input.EntityId);
        var result = await _blobRepository.GetListAsync(input.ContainerName,input.Filter,input.EntityTypeFullName,input.EntityId,input.Sorting,input.MaxResultCount,input.SkipCount);

        return new PagedResultDto<FileDescriptorDto>(
            count,
            ObjectMapper.Map<List<FileDescriptor>, List<FileDescriptorDto>>(result)
            );
    }

    public async Task DeleteAsync(Guid id)
    {
        var result = await _blobRepository.GetAsync(id,false);
        await _fileManager.DeleteAsync(result);
    }



    public virtual Task<BlobContainerConfigurationDto> GetBlobContainerConfigurationAsync([NotNull] string containerName)
    {
        var configuration = _blobContainerConfigurationProvider.Get(containerName);
        var blobSizeLimitHandlerConfiguration = new BlobSizeLimitHandlerConfiguration(configuration);
        var fileTypeCheckHandlerConfiguration = new FileTypeCheckHandlerConfiguration(configuration);
        var imageProcessHandlerConfiguration = new ImageResizeHandlerConfiguration(configuration);

        return Task.FromResult(
            new BlobContainerConfigurationDto(
                new BlobSizeLimitHandlerConfigurationDto(blobSizeLimitHandlerConfiguration.MaximumBlobSize),
                new FileTypeCheckHandlerConfigurationDto(fileTypeCheckHandlerConfiguration.AllowedFileTypeNames),
                new ImageProcessHandlerConfigurationDto(imageProcessHandlerConfiguration.ImageWidth, imageProcessHandlerConfiguration.ImageHeight, imageProcessHandlerConfiguration.ImageSizeMustBeLargerThanPreset)
                )
            );
    }


    private static void ThrowValidationException(string message, string memberName)
    {
        throw new AbpValidationException(message,
            new List<ValidationResult>
            {
                    new ValidationResult(message, new[] {memberName})
            });
    }
}
