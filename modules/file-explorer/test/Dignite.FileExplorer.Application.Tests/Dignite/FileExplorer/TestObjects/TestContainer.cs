using Volo.Abp.BlobStoring;

namespace Dignite.FileExplorer.TestObjects;

[BlobContainerName(Name)]
public class TestContainer
{
    public const string Name = "TestContainer";
}
