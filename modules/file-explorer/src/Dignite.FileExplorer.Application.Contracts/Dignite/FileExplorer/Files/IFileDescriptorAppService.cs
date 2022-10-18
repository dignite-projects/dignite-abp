using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;

namespace Dignite.FileExplorer.Files;

public interface IFileDescriptorAppService : IApplicationService
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="containerName"></param>
    /// <param name="blobName"></param>
    /// <returns></returns>
    Task<IRemoteStreamContent> GetFileAsync([NotNull] string containerName, [NotNull] string blobName);

    /// <summary>
    ///
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<PagedResultDto<FileDescriptorDto>> GetListAsync(GetFilesInput input);

    /// <summary>
    ///
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task DeleteAsync(Guid id);


    /*
    TODO: 
    1:Volume statistics by tenant group
    2:Volume statistics by container group
    3:Volume statistics by user group
    4:Calculate the Volume of the specified tenant
    4:Calculate the Volume of the specified user
     */
}