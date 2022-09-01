using System;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.BlobStoring.InfoPersistent;

public class CurrentBlobInfo : ICurrentBlobInfo, ITransientDependency
{
    public virtual bool IsAvailable => BlobInfo != null;

    public virtual IBlobInfo BlobInfo => _currentTenantAccessor.Current;

    private readonly ICurrentBlobInfoAccessor _currentTenantAccessor;

    public CurrentBlobInfo(ICurrentBlobInfoAccessor currentTenantAccessor)
    {
        _currentTenantAccessor = currentTenantAccessor;
    }

    public IDisposable Current(IBlobInfo blobInfo)
    {
        var parentScope = _currentTenantAccessor.Current;
        _currentTenantAccessor.Current = blobInfo;
        return new DisposeAction(() =>
        {
            _currentTenantAccessor.Current = parentScope;
        });
    }

}
