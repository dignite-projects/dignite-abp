using System.Threading.Tasks;
using Dignite.Abp.Files;
using Dignite.FileExplorer.Files;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.FileStoring;

/// <summary>
/// </summary>
public class EntityIdBlobNameGenerator : IBlobNameGenerator, ITransientDependency
{
    private readonly ICurrentFile _currentFile;

    public EntityIdBlobNameGenerator(ICurrentFile currentFile)
    {
        _currentFile = currentFile;
    }

    public virtual Task<string> Create()
    {
        var file = (FileDescriptor)_currentFile.FileDescriptor;

        return Task.FromResult(file.EntityId);
    }
}
