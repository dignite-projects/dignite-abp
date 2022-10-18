using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

    public async Task<DirectoryDescriptorDto> CreateAsync(CreateDirectoryInput input)
    {
        var entity= await _directoryManager.CreateAsync(input.ContainerName,input.Name,input.ParentId);
        return
            ObjectMapper.Map<DirectoryDescriptor, DirectoryDescriptorDto>(entity);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _directoryManager.DeleteAsync(id);
    }

    public async Task<DirectoryDescriptorDto> GetAsync(Guid id)
    {
        var entity = await _directoryRepository.GetAsync(id);
        return
            ObjectMapper.Map<DirectoryDescriptor, DirectoryDescriptorDto>(entity);
    }

    public async Task<PagedResultDto<DirectoryDescriptorInfoDto>> GetListAsync(GetDirectoriesInput input)
    {
        var count = await _directoryRepository.GetChildrenCountAsync(input.ContainerName, input.ParentId);
        var result = await _directoryRepository.GetChildrenListAsync(input.ContainerName, input.ParentId, input.MaxResultCount, input.SkipCount);
        var dtoList = ObjectMapper.Map<List<DirectoryDescriptor>, List<DirectoryDescriptorInfoDto>>(result);
        foreach (var dto in dtoList)
        {
            dto.HasChildren = await _directoryRepository.AnyChildrenAsync(input.ContainerName, input.ParentId);
        }

        return new PagedResultDto<DirectoryDescriptorInfoDto>(count, dtoList);
    }

    public async Task<DirectoryDescriptorDto> MoveAsync(MoveDirectoryInput input)
    {
        var entity = await _directoryRepository.GetAsync(input.Id,false);
        await _directoryManager.MoveAsync(entity, input.NewParentId, input.Order);
        return
            ObjectMapper.Map<DirectoryDescriptor, DirectoryDescriptorDto>(entity);
    }

    public async Task<DirectoryDescriptorDto> UpdateAsync(Guid id, UpdateDirectoryInput input)
    {
        var entity = await _directoryRepository.GetAsync(id, false);
        await _directoryManager.RenameAsync(entity,input.Name);
        return
            ObjectMapper.Map<DirectoryDescriptor, DirectoryDescriptorDto>(entity);
    }
}
