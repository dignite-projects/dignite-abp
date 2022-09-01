
using System.Threading;

namespace Dignite.Abp.Files;

public class AsyncLocalCurrentFileAccessor : ICurrentFileAccessor
{
    public static AsyncLocalCurrentFileAccessor Instance { get; } = new();

    public IFile Current {
        get => _currentScope.Value;
        set => _currentScope.Value = value;
    }

    private readonly AsyncLocal<IFile> _currentScope;

    private AsyncLocalCurrentFileAccessor()
    {
        _currentScope = new AsyncLocal<IFile>();
    }
}
