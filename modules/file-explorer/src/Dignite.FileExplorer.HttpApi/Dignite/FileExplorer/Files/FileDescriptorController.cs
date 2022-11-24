using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Content;

namespace Dignite.FileExplorer.Files;

[Area("FileExplorer")]
[RemoteService(Name = FileExplorerRemoteServiceConsts.RemoteServiceName)]
[Route("api/file-explorer/files")]
public class FileDescriptorController : AbpController, IFileDescriptorAppService
{
    private readonly IFileDescriptorAppService _fileAppService;

    public FileDescriptorController(
        IFileDescriptorAppService fileAppService
        )
    {
        _fileAppService = fileAppService;
    }

    [HttpPost]
    public async Task<FileDescriptorDto> CreateAsync(CreateFileInput input)
    {
        return await _fileAppService.CreateAsync(input);
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<FileDescriptorDto> UpdateAsync(Guid id, UpdateFileInput input)
    {
        return await _fileAppService.UpdateAsync(id, input);
    }

    [HttpDelete]
    [Route("{id}")]
    public virtual async Task DeleteAsync(Guid id)
    {
        await _fileAppService.DeleteAsync(id);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<FileDescriptorDto> GetAsync(Guid id)
    {
        return await _fileAppService.GetAsync(id);
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet]
    public virtual async Task<PagedResultDto<FileDescriptorDto>> GetListAsync(GetFilesInput input)
    {
        var result = await _fileAppService.GetListAsync(input);

        return result;
    }

    [HttpGet]
    [Route("{containerName}/{*blobName}")]
    public virtual async Task<IRemoteStreamContent> GetStreamAsync([NotNull] string containerName, [NotNull] string blobName)
    {
        return await _fileAppService.GetStreamAsync(containerName, blobName);
    }

    [HttpGet]
    [Route("download/{containerName}/{*blobName}")]
    public virtual async Task<FileResult> DownloadAsync([NotNull] string containerName, [NotNull] string blobName, [NotNull] string fileName)
    {
        var file = await _fileAppService.GetStreamAsync(containerName, blobName);
        return File(file.GetStream(), file.ContentType, fileName.IsNullOrEmpty() ? file.FileName : fileName);
    }

    [HttpGet]
    [Route("configuration")]
    public virtual async Task<FileContainerConfigurationDto> GetFileContainerConfiguration([NotNull] string containerName)
    {
        return await _fileAppService.GetFileContainerConfiguration(containerName);
    }
}