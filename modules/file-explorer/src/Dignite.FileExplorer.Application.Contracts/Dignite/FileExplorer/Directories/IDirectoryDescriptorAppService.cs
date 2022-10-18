using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Dignite.FileExplorer.Directories;
public interface IDirectoryDescriptorAppService: ICrudAppService<DirectoryDescriptorDto,DirectoryDescriptorInfoDto, Guid, GetDirectoriesInput, CreateDirectoryInput, UpdateDirectoryInput>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<DirectoryDescriptorDto> MoveAsync(MoveDirectoryInput input);

}
