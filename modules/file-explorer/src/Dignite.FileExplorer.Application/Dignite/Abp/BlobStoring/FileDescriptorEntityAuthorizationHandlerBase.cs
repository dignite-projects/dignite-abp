using System.Threading.Tasks;
using Dignite.FileExplorer.Files;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.BlobStoring;

public abstract class FileDescriptorEntityAuthorizationHandlerBase<TResource> : IFileDescriptorEntityAuthorizationHandler
{
    public IAbpLazyServiceProvider LazyServiceProvider { get; set; }
    protected IAuthorizationService AuthorizationService => LazyServiceProvider.LazyGetRequiredService<IAuthorizationService>();

    public async Task CheckAsync(FileDescriptor file, IAuthorizationRequirement requirement)
    {
        var resource = await GetResourceAsync(file);
        await AuthorizationService.CheckAsync(resource, requirement);
    }

    public abstract Task<TResource> GetResourceAsync(FileDescriptor file);
}