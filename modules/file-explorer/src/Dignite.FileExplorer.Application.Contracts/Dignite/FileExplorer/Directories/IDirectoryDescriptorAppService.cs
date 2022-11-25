using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Dignite.FileExplorer.Directories;

public interface IDirectoryDescriptorAppService : ICrudAppService<DirectoryDescriptorDto, DirectoryDescriptorInfoDto, Guid, GetDirectoriesInput, CreateDirectoryInput, UpdateDirectoryInput>
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="id"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<DirectoryDescriptorDto> MoveAsync(Guid id, MoveDirectoryInput input);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="containerName"></param>
    /// <returns></returns>
    Task<ListResultDto<DirectoryDescriptorInfoDto>> GetMyAsync(string containerName);
}