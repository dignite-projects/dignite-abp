using System.Threading.Tasks;
using Shouldly;
using Volo.Abp.Modularity;
using Xunit;

namespace Dignite.FileExplorer.Files;

/* Write your custom repository tests like that, in this project, as abstract classes.
 * Then inherit these abstract classes from EF Core & MongoDB test projects.
 * In this way, both database providers are tests with the same set tests.
 */

public abstract class FileDescriptorRepository_Tests<TStartupModule> : FileExplorerTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{
    private readonly FileExplorerTestData testData;
    private readonly IFileDescriptorRepository _fileDescriptorRepository;

    protected FileDescriptorRepository_Tests()
    {
        _fileDescriptorRepository = GetRequiredService<IFileDescriptorRepository>();
        testData = GetRequiredService<FileExplorerTestData>();
    }

    [Fact]
    public async Task BlobNameExistsAsync_ShouldReturnTrue_WithExistingBlobName()
    {
        var result = await _fileDescriptorRepository.BlobNameExistsAsync(testData.ContainerName1, testData.BlobName1);

        result.ShouldBeTrue();
    }
}