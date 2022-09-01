
using System.Threading;

namespace Dignite.Abp.BlobStoring.InfoPersistent;

public class AsyncLocalCurrentBlobInfoAccessor : ICurrentBlobInfoAccessor
{
    public static AsyncLocalCurrentBlobInfoAccessor Instance { get; } = new();

    public IBlobInfo Current {
        get => _currentScope.Value;
        set => _currentScope.Value = value;
    }

    private readonly AsyncLocal<IBlobInfo> _currentScope;

    private AsyncLocalCurrentBlobInfoAccessor()
    {
        _currentScope = new AsyncLocal<IBlobInfo>();
    }
}
