using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Content;

namespace Dignite.FileExplorer.Files;

[Area(FileExplorerRemoteServiceConsts.ModuleName)]
[RemoteService(Name = FileExplorerRemoteServiceConsts.RemoteServiceName)]
[Route(RoutePrefix)]
public class FileDescriptorController : AbpController, IFileDescriptorAppService
{
    private const string RoutePrefix = "api/file-explorer/files";
    private readonly IFileDescriptorAppService _fileAppService;

    public FileDescriptorController(
        IFileDescriptorAppService fileAppService
        )
    {
        _fileAppService = fileAppService;
    }

    [IgnoreAntiforgeryToken]
    [HttpPost]
    public async Task<FileDescriptorDto> CreateAsync(CreateFileInput input)
    {
        var file = await _fileAppService.CreateAsync(input);
        file.Url = GetFileUrl(file);
        return file;
    }

    [HttpPut]
    [Route("{id:guid}")]
    public async Task<FileDescriptorDto> UpdateAsync(Guid id, UpdateFileInput input)
    {
        return await _fileAppService.UpdateAsync(id, input);
    }

    [HttpDelete]
    [Route("{id:guid}")]
    public virtual async Task DeleteAsync(Guid id)
    {
        await _fileAppService.DeleteAsync(id);
    }

    [HttpGet]
    [Route("{id:guid}")]
    public async Task<FileDescriptorDto> GetAsync(Guid id)
    {
        var file = await _fileAppService.GetAsync(id);
        file.Url = GetFileUrl(file);
        return file;
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
        foreach (var item in result.Items)
        {
            item.Url = GetFileUrl(item);
        }
        return result;
    }

    [HttpGet]
    [Route("configuration")]
    public virtual async Task<FileContainerConfigurationDto> GetFileContainerConfigurationAsync([NotNull] string containerName)
    {
        return await _fileAppService.GetFileContainerConfigurationAsync(containerName);
    }

    [HttpGet]
    [Route("download/{containerName}/{*blobName}")]
    public virtual async Task<FileResult> DownloadAsync([NotNull] string containerName, [NotNull] string blobName, [NotNull] string fileName)
    {
        var file = await _fileAppService.GetStreamAsync(containerName, blobName);
        return File(file.GetStream(), file.ContentType, fileName.IsNullOrEmpty() ? file.FileName : fileName);
    }

    [HttpGet]
    [Route("{containerName}/{*blobName}")]
    public virtual async Task<IRemoteStreamContent> GetStreamAsync([NotNull] string containerName, [NotNull] string blobName)
    {
        return await _fileAppService.GetStreamAsync(containerName, blobName);
    }

    private string GetFileUrl(FileDescriptorDto file)
    {
        return $"{Request.Scheme}://{Request.Host.Value}/{RoutePrefix}/{file.ContainerName}/{file.BlobName}";
    }
}