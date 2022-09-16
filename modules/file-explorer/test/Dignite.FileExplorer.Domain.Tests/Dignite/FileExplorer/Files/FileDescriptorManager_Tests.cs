using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.BlobStoring;
using Volo.Abp.Content;
using Xunit;
using Shouldly;

namespace Dignite.FileExplorer.Files;

public class FileDescriptorManager_Tests : FileExplorerDomainTestBase
{
    private readonly FileDescriptorManager _fileDescriptorManager;

    public FileDescriptorManager_Tests()
    {
        _fileDescriptorManager = GetRequiredService<FileDescriptorManager>();
    }

    [Fact]
    public async Task CreateAsync_ShouldWorkProperly_WithCorrectData()
    {
        var memoryStream = new MemoryStream();
        await memoryStream.WriteAsync(Encoding.UTF8.GetBytes("text content"));
        memoryStream.Position = 0;

        var streams = new List<IRemoteStreamContent>() {
            new RemoteStreamContent(memoryStream, "text.txt", "text/plain")
            };


        var files = await _fileDescriptorManager.CreateAsync<DefaultContainer>(
            new FakeEntity(Guid.NewGuid()),
            streams
            );

        files.ShouldNotBeEmpty();
    }
}