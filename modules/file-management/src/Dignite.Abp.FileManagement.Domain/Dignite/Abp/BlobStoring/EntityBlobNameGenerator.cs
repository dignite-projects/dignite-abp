using System.Threading.Tasks;
using Dignite.Abp.BlobStoring.InfoPersistent;
using Dignite.Abp.FileManagement.Files;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.BlobStoring;

/// <summary>
/// </summary>
public class EntityIdBlobNameGenerator : IBlobNameGenerator, ITransientDependency
{
    private readonly ICurrentBlobInfo _currentFile;

    public EntityIdBlobNameGenerator(ICurrentBlobInfo currentFile)
    {
        _currentFile = currentFile;
    }

    public virtual Task<string> Create()
    {
        var file = (FileDescriptor)_currentFile.BlobInfo;

        return Task.FromResult(file.EntityId);
    }
}
