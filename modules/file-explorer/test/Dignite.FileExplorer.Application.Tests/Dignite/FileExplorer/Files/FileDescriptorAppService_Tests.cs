using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace Dignite.FileExplorer.Files;

public class FileDescriptorAppService_Tests : FileExplorerApplicationTestBase
{
    private readonly IFileDescriptorAppService _fileDescriptorAppService;

    public FileDescriptorAppService_Tests()
    {
        _fileDescriptorAppService = GetRequiredService<IFileDescriptorAppService>();
    }

    [Fact]
    public async Task ShouldGetListAsync()
    {
        var result = await _fileDescriptorAppService.GetListAsync(new GetFilesInput()
        {
            ContainerName = "testContainer1"
        });
        result.Items.ShouldNotBeEmpty();
    }
}