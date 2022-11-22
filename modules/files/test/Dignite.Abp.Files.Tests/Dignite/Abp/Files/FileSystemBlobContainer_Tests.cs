using System;
using System.Threading.Tasks;
using Dignite.Abp.Files.Fakes;
using Shouldly;
using Volo.Abp;
using Xunit;

namespace Dignite.Abp.Files;

public class FileSystemBlobContainer_Tests : AbpFilesDomainTestBase
{
    protected FakeFileManager FakeFileManager { get; }

    public FileSystemBlobContainer_Tests()
    {
        FakeFileManager = GetRequiredService<FakeFileManager>();
    }

    [Fact]
    public async Task Limit_Size_Of_Blob()
    {
        var blobName = "test-blob-1";
        var bytes = "test content".GetBytes();
        var newFile = new FakeFile(Guid.NewGuid(), "TestContainer2", blobName, blobName + ".txt", "text/plain");

        await FakeFileManager.CreateAsync(newFile, bytes);

        var stream = await FakeFileManager.GetStreamOrNullAsync(newFile.ContainerName, blobName);

        stream.ShouldNotBeNull();
    }

    [Fact]
    public async Task Constraint_Blob_File_Type()
    {
        var blobName = "test-blob-1.txt";
        var bytes = "test content".GetBytes();

        var newFile = new FakeFile(Guid.NewGuid(), "TestContainer3", blobName, blobName, "text/plain");

        await Assert.ThrowsAsync<BusinessException>(() =>
            FakeFileManager.CreateAsync(newFile, bytes)
        );
    }

}