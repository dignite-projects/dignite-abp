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
        var userId = CurrentUser.Id.Value;
        var count = await _directoryRepository.GetCountAsync(userId, input.ContainerName, input.ParentId);
        var result = await _directoryRepository.GetListAsync(userId, input.ContainerName, input.ParentId, input.SkipCount, input.MaxResultCount);
        var dtoList = ObjectMapper.Map<List<DirectoryDescriptor>, List<DirectoryDescriptorInfoDto>>(result);
        foreach (var dto in dtoList)
        {
            dto.HaveChildren(await _directoryRepository.AnyChildrenAsync(userId, input.ContainerName, dto.Id));
        }

        return new PagedResultDto<DirectoryDescriptorInfoDto>(count, dtoList);
    }

    [Authorize]
    public async Task<DirectoryDescriptorDto> MoveAsync(Guid id, MoveDirectoryInput input)
    {
        var entity = await _directoryRepository.GetAsync(id, false);
        var target = await _directoryRepository.GetAsync(input.TargetId,false);
        await AuthorizationService.CheckAsync(entity, CommonOperations.Update);
        await AuthorizationService.CheckAsync(target, CommonOperations.Update);

        if (!entity.ContainerName.Equals(target.ContainerName, StringComparison.OrdinalIgnoreCase))
        {
            throw new DirectoryInvalidMoveException();
        }

        if (input.Position == DirectoryMovePosition.Inside)
        {
            var order = await _directoryRepository.GetMaxOrderAsync(entity.CreatorId.Value, entity.ContainerName, target.Id);
            entity.Order = order;
            await _directoryManager.MoveAsync(entity, target.Id);
        }
        else if (input.Position == DirectoryMovePosition.Bottom)
        { 
            var children = await _directoryRepository.GetListAsync(entity.CreatorId.Value,entity.ContainerName,target.ParentId,0,1000);
            entity.Order = target.Order+1;
            await _directoryManager.MoveAsync(entity, target.ParentId);
            foreach (var directory in children)
            {
                if (directory.Order >= entity.Order)
                {
                    directory.Order= directory.Order+1;
                    await _directoryRepository.UpdateAsync(directory);
                }
            }
        }

        return ObjectMapper.Map<DirectoryDescriptor, DirectoryDescriptorDto>(entity);
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