using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;

namespace Dignite.FileExplorer.Directories;

public class DirectoryDescriptorAppService : FileExplorerAppService, IDirectoryDescriptorAppService
{
    private readonly IDirectoryManager _directoryManager;
    private readonly IDirectoryDescriptorRepository _directoryRepository;

    public DirectoryDescriptorAppService(IDirectoryManager directoryManager, IDirectoryDescriptorRepository directoryRepository)
    {
        _directoryManager = directoryManager;
        _directoryRepository = directoryRepository;
    }

    [Authorize]
    public async Task<DirectoryDescriptorDto> CreateAsync(CreateDirectoryInput input)
    {
        var entity = await _directoryManager.CreateAsync(CurrentUser.Id.Value, input.ContainerName, input.Name, input.ParentId);
        await AuthorizationService.CheckAsync(entity, CommonOperations.Create);
        return
            ObjectMapper.Map<DirectoryDescriptor, DirectoryDescriptorDto>(entity);
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
        var userId = CurrentUser.Id.Value;
        var count = await _directoryRepository.GetChildrenCountAsync(userId, input.ContainerName, input.ParentId);
        var result = await _directoryRepository.GetChildrenListAsync(userId, input.ContainerName, input.ParentId, input.MaxResultCount, input.SkipCount);
        var dtoList = ObjectMapper.Map<List<DirectoryDescriptor>, List<DirectoryDescriptorInfoDto>>(result);
        foreach (var dto in dtoList)
        {
            dto.HasChildren = await _directoryRepository.AnyChildrenAsync(userId, input.ContainerName, input.ParentId);
        }

        return new PagedResultDto<DirectoryDescriptorInfoDto>(count, dtoList);
    }

    [Authorize]
    public async Task<DirectoryDescriptorDto> MoveAsync(MoveDirectoryInput input)
    {
        var entity = await _directoryRepository.GetAsync(input.Id, false);
        await AuthorizationService.CheckAsync(entity, CommonOperations.Update);

        if (input.NewParentId.HasValue)
        {
            var newParent = await _directoryRepository.GetAsync(input.NewParentId.Value, false);
            await AuthorizationService.CheckAsync(newParent, CommonOperations.Update);
        }

        await _directoryManager.MoveAsync(entity, input.NewParentId, input.Order);
        return
            ObjectMapper.Map<DirectoryDescriptor, DirectoryDescriptorDto>(entity);
    }

    [Authorize]
    public async Task<DirectoryDescriptorDto> UpdateAsync(Guid id, UpdateDirectoryInput input)
    {
        var entity = await _directoryRepository.GetAsync(id, false);
        await AuthorizationService.CheckAsync(entity, CommonOperations.Update);
        await _directoryManager.RenameAsync(entity, input.Name);
        return
            ObjectMapper.Map<DirectoryDescriptor, DirectoryDescriptorDto>(entity);
    }
}