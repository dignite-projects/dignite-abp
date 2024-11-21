using System;
using System.Collections.Generic;
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

    [HttpDelete]
    [Route("{containerName}/{entityId}")]
    public virtual async Task DeleteByEntityIdAsync([NotNull] string containerName, [NotNull] string entityId)
    {
        await _fileAppService.DeleteByEntityIdAsync(containerName, entityId);
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
    [Route("{containerName}/{entityId}/all")]
    public async Task<ListResultDto<FileDescriptorDto>> GetListByEntityIdAsync([NotNull] string containerName, [NotNull] string entityId)
    {
        return await _fileAppService.GetListByEntityIdAsync(containerName,entityId);
    }

    [HttpGet]
    [Route("{containerName}/configuration")]
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
    public virtual async Task<IRemoteStreamContent> GetStreamAsync([NotNull] string containerName, [NotNull] string blobName, ImageResizeInput imageResize = null)
    {
        return await _fileAppService.GetStreamAsync(containerName, blobName, imageResize);
    }

    private string GetFileUrl(FileDescriptorDto file)
    {
        return $"{Request.Scheme}://{Request.Host.Value}/{RoutePrefix}/{file.ContainerName}/{file.BlobName}?__tenant={file.TenantId}";
    }
}