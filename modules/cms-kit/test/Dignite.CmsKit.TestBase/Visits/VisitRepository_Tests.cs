using System.Threading.Tasks;
using Shouldly;
using Volo.Abp.Modularity;
using Xunit;

namespace Dignite.CmsKit.Visits;

public abstract class VisitRepository_Tests<TStartupModule> : CmsKitTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{
    private readonly CmsKitTestData _cmsKitTestData;
    private readonly IVisitRepository _visitRepository;

    public VisitRepository_Tests()
    {
        _cmsKitTestData = GetRequiredService<CmsKitTestData>();
        _visitRepository = GetRequiredService<IVisitRepository>();
    }

    [Fact]
    public async Task GetListAsync()
    {
        var result = await _visitRepository.GetListAsync(_cmsKitTestData.EntityType1);

        result.ShouldNotBeNull();
        result.Count.ShouldBe(1);
    }
}
