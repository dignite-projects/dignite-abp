using System.Threading.Tasks;
using Dignite.Abp.BlobStoring;
using Dignite.FileExplorer.TestObjects;
using Shouldly;
using Volo.Abp.BlobStoring;
using Xunit;

namespace Dignite.FileExplorer;

public class BlobContainerAuthorizationConfiguration_Tests : FileExplorerApplicationTestBase
{
    private readonly IBlobContainerConfigurationProvider _blobContainerConfigurationProvider;

    public BlobContainerAuthorizationConfiguration_Tests()
    {
        _blobContainerConfigurationProvider = GetRequiredService<IBlobContainerConfigurationProvider>();
    }

    [Fact]
    public Task ShouldGetAuthorizationConfigurationAsync()
    {
        var configuration = _blobContainerConfigurationProvider.Get<TestContainer1>();
        var authorizationConfiguration = configuration.GetAuthorizationConfiguration();

        authorizationConfiguration.CreateDirectoryPermissionName.ShouldNotBeEmpty();

        authorizationConfiguration.FileEntityAuthorizationHandler.ShouldNotBeNull();

        return Task.CompletedTask;
    }
}