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

}