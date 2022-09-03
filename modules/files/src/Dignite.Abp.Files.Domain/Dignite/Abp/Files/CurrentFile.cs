using System;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.Files;

public class CurrentFile : ICurrentFile, ITransientDependency
{
    public virtual bool IsAvailable => File != null;

    public virtual IFile File => _currentTenantAccessor.Current;

    private readonly ICurrentFileAccessor _currentTenantAccessor;

    public CurrentFile(ICurrentFileAccessor currentTenantAccessor)
    {
        _currentTenantAccessor = currentTenantAccessor;
    }

    public IDisposable Current(IFile file)
    {
        var parentScope = _currentTenantAccessor.Current;
        _currentTenantAccessor.Current = file;
        return new DisposeAction(() =>
        {
            _currentTenantAccessor.Current = parentScope;
        });
    }

}
