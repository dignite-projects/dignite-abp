using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;

namespace Dignite.FileExplorer.Directories;

public class DirectoryDescriptorAppService : FileExplorerAppService, IDirectoryDescriptorAppService
{
    private readonly DirectoryManager _directoryManager;
    private readonly IDirectoryDescriptorRepository _directoryRepository;

    public DirectoryDescriptorAppService(DirectoryManager directoryManager, IDirectoryDescriptorRepository directoryRepository)
    {
        _directoryManager = directoryManager;
        _directoryRepository = directoryRepository;
    }

    [Authorize]
    public async Task<DirectoryDescriptorDto> CreateAsync(CreateDirectoryInput input)
    {
        var entity = await _directoryManager.CreateAsync(CurrentUser.Id.Value, input.ContainerName, input.Name, input.ParentId);
        await AuthorizationService.CheckAsync(entity, CommonOperations.Create);
        return ObjectMapper.Map<DirectoryDescriptor, DirectoryDescriptorDto>(entity);
    }

    [Authorize]
    public async Task DeleteAsync(Guid id)
    {
        var entity = await _directoryRepository.GetAsync(id);
        await AuthorizationService.CheckAsync(entity, CommonOperations.Delete);
        await _directoryRepository.DeleteAsync(entity);
    }

    [Authorize]
    public async Task<DirectoryDescriptorDto> GetAsync(Guid id)
    {
        var entity = await _directoryRepository.GetAsync(id);
        await AuthorizationService.CheckAsync(entity, CommonOperations.Get);
        return
            ObjectMapper.Map<DirectoryDescriptor, DirectoryDescriptorDto>(entity);
    }

    [Authorize]
    public async Task<PagedResultDto<DirectoryDescriptorInfoDto>> GetListAsync(GetDirectoriesInput input)
    {
        var result = await _directoryRepository.GetAllByUserAsync(CurrentUser.Id.Value, input.ContainerName);
        var dtoList = ObjectMapper.Map<List<DirectoryDescriptor>, List<DirectoryDescriptorInfoDto>>(result);
        return new PagedResultDto<DirectoryDescriptorInfoDto>(dtoList.Count, dtoList.BuildTree());
    }

    [Authorize]
    public async Task<DirectoryDescriptorDto> MoveAsync(Guid id, MoveDirectoryInput input)
    {
        var entity = await _directoryRepository.GetAsync(id, false);
        await AuthorizationService.CheckAsync(entity, CommonOperations.Update);
        if (input.ParentId.HasValue)
        {
            var parent = await _directoryRepository.GetAsync(input.ParentId.Value, false);
            await AuthorizationService.CheckAsync(parent, CommonOperations.Update);
        }
        entity = await _directoryManager.MoveAsync(entity, input.ParentId, input.Order);
        return ObjectMapper.Map<DirectoryDescriptor, DirectoryDescriptorDto>(entity);
    }

    [Authorize]
    public async Task<DirectoryDescriptorDto> UpdateAsync(Guid id, UpdateDirectoryInput input)
    {
        var entity = await _directoryRepository.GetAsync(id, false);
        await AuthorizationService.CheckAsync(entity, CommonOperations.Update);
        await _directoryManager.UpdateAsync(entity, input.Name);
        return
            ObjectMapper.Map<DirectoryDescriptor, DirectoryDescriptorDto>(entity);
    }
}