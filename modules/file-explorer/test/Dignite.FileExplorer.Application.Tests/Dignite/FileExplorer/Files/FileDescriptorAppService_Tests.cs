using System.Threading.Tasks;
using Dignite.FileExplorer.Files;
using Shouldly;
using Xunit;

namespace Dignite.FileExplorer.Samples;

public class FileDescriptorAppService_Tests : FileExplorerApplicationTestBase
{
    private readonly IFileDescriptorAppService _fileDescriptorAppService;

    public FileDescriptorAppService_Tests()
    {
        _fileDescriptorAppService = GetRequiredService<IFileDescriptorAppService>();
    }

    [Fact]
    public async Task GetAsync()
    {
        var result = await _fileDescriptorAppService.GetListAsync(new GetFilesInput ());
        result.TotalCount.ShouldBe(42);
    }

}
