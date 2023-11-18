using System;
using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace Dignite.CmsKit.Visits;

public class VisitManager_Test : CmsKitDomainTestBase
{
    private readonly CmsKitTestData _cmsKitTestData;
    private readonly VisitManager _visitManager;

    public VisitManager_Test()
    {
        _cmsKitTestData = GetRequiredService<CmsKitTestData>();
        _visitManager = GetRequiredService<VisitManager>();
    }

    [Fact]
    public async Task CreateAsync_ShouldCreate_WhenFirstCall()
    {
        var visit = await _visitManager.CreateAsync(_cmsKitTestData.EntityType1, _cmsKitTestData.EntityId1, 
            "Mozilla/5.0 (Windows NT 6.3; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/109.0.5410.0 Safari/537.36",
            "22.229.171.46", 
            60, _cmsKitTestData.User1Id);

        visit.ShouldNotBeNull();
        visit.Id.ShouldNotBe(Guid.Empty);
    }

    [Fact]
    public async Task CreateAsync_ShouldThrowException_WithNotConfiguredentityType()
    {
        var notConfiguredEntityType = "AnyOtherEntityType";

        var exception = await Should.ThrowAsync<EntityCantHaveVisitException>(async () =>
                            await _visitManager.CreateAsync( notConfiguredEntityType, "1",
                            "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/109.0.5410.0 Safari/537.36",
                            "179.45.177.180",
                            50,
                            _cmsKitTestData.User1Id
                            ));

        exception.ShouldNotBeNull();
        exception.EntityType.ShouldBe(notConfiguredEntityType);
    }
}
