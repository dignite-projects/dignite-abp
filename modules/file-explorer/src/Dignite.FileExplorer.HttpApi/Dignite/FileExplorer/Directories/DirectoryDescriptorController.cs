using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace Dignite.FileExplorer.Directories;

[Area(FileExplorerRemoteServiceConsts.ModuleName)]
[RemoteService(Name = FileExplorerRemoteServiceConsts.RemoteServiceName)]
[Route("api/file-explorer/directories")]
public class FileDescriptorController : AbpController, IDirectoryDescriptorAppService
{
    private readonly IDirectoryDescriptorAppService _directoryAppService;

    public FileDescriptorController(
        IDirectoryDescriptorAppService directoryAppService
        )
    {
        _directoryAppService = directoryAppService;
    }

    [HttpPost]
    public async Task<DirectoryDescriptorDto> CreateAsync(CreateDirectoryInput input)
    {
        return await _directoryAppService.CreateAsync(input);
    }

    [HttpDelete]
    [Route("{id:guid}")]
    public async Task DeleteAsync(Guid id)
    {
        await _directoryAppService.DeleteAsync(id);
    }

    [HttpGet]
    [Route("{id:guid}")]
    public async Task<DirectoryDescriptorDto> GetAsync(Guid id)
    {
        return await _directoryAppService.GetAsync(id);
    }

    [HttpGet]
    public async Task<PagedResultDto<DirectoryDescriptorInfoDto>> GetListAsync(GetDirectoriesInput input)
    {
        return await _directoryAppService.GetListAsync(input);
    }


    [HttpPut]
    [Route("{id:guid}/move")]
    public async Task<DirectoryDescriptorDto> MoveAsync(Guid id,MoveDirectoryInput input)
    {
        return await _directoryAppService.MoveAsync(id, input);
    }

    [HttpPut]
    [Route("{id:guid}")]
    public async Task<DirectoryDescriptorDto> UpdateAsync(Guid id, UpdateDirectoryInput input)
    {
        return await _directoryAppService.UpdateAsync(id, input);
    }
}