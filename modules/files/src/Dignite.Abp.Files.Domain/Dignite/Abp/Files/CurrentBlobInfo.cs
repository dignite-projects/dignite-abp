using System;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.Files;

public class CurrentBlobInfo : ICurrentFile, ITransientDependency
{
    public virtual bool IsAvailable => FileDescriptor != null;

    public virtual IFile FileDescriptor => _currentTenantAccessor.Current;

    private readonly ICurrentFileAccessor _currentTenantAccessor;

    public CurrentBlobInfo(ICurrentFileAccessor currentTenantAccessor)
    {
        _currentTenantAccessor = currentTenantAccessor;
    }

    public IDisposable Current(IFile blobInfo)
    {
        var parentScope = _currentTenantAccessor.Current;
        _currentTenantAccessor.Current = blobInfo;
        return new DisposeAction(() =>
        {
            _currentTenantAccessor.Current = parentScope;
        });
    }

}
