using System.Threading.Tasks;
using Dignite.FileExplorer.Files;
using Microsoft.AspNetCore.Authorization;

namespace Dignite.Abp.BlobStoring;

/// <summary>
/// Check the handler authorization by the file associated entity
/// </summary>
public interface IFileDescriptorEntityAuthorizationHandler
{
    /// <summary>
    /// Check authorization
    /// </summary>
    /// <param name="file"></param>
    /// <param name="requirement"></param>
    /// <returns></returns>
    Task CheckAsync(FileDescriptor file, IAuthorizationRequirement requirement);
}