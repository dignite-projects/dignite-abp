using System.Linq;
using System.Threading.Tasks;
using Dignite.CmsKit.Public.Visits;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Shouldly;
using Volo.Abp.Users;
using Xunit;

namespace Dignite.CmsKit.Visits;

public class VisitPublicAppService_Tests : CmsKitApplicationTestBase
{
    private readonly IVisitPublicAppService _visitAppService;
    private ICurrentUser _currentUser;
    private readonly CmsKitTestData _cmsKitTestData;

    public VisitPublicAppService_Tests()
    {
        _visitAppService = GetRequiredService<IVisitPublicAppService>();
        _cmsKitTestData = GetRequiredService<CmsKitTestData>();
    }

    protected override void AfterAddApplication(IServiceCollection services)
    {
        _currentUser = Substitute.For<ICurrentUser>();
        services.AddSingleton(_currentUser);
    }

    [Fact]
    public async Task CreateByMemberAsync()
    {
        _currentUser.Id.Returns(_cmsKitTestData.User1Id);

        var newVisit = await _visitAppService.CreateAsync(
            _cmsKitTestData.EntityType1,
            _cmsKitTestData.EntityId2,
            new CreateVisitInput
            {
                ClientIpAddress = "127.0.0.1",
                UserAgent = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10.15; rv:83.0) Gecko/20100101 Firefox/83.0",
                Duration = 70
            }
            );

        UsingDbContext(context =>
        {
            var visits = context.Set<Visit>().Where(x =>
                x.EntityId == _cmsKitTestData.EntityId2 && x.EntityType == _cmsKitTestData.EntityType1).ToList();

            visits
                .Any(c => c.Id == newVisit.Id && c.CreatorId == newVisit.CreatorId)
                .ShouldBeTrue();
        });
    }

    [Fact]
    public async Task CreateByNonMemberAsync()
    {
        var newVisit = await _visitAppService.CreateAsync(
            _cmsKitTestData.EntityType1,
            _cmsKitTestData.EntityId2,
            new CreateVisitInput
            {
                ClientIpAddress = "234.140.199.210",
                UserAgent = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10.15; rv:83.0) Gecko/20100101 Firefox/83.0",
                Duration = 30
            }
            );

        UsingDbContext(context =>
        {
            var visits = context.Set<Visit>().Where(x =>
                x.EntityId == _cmsKitTestData.EntityId2 && x.EntityType == _cmsKitTestData.EntityType1).ToList();

            visits
                .Any(c => c.Id == newVisit.Id)
                .ShouldBeTrue();
        });
    }

    [Fact]
    public async Task GetListByCurrentUserAsync()
    {
        _currentUser.Id.Returns(_cmsKitTestData.User1Id);

        var vists = await _visitAppService.GetListByCurrentUserAsync(
            _cmsKitTestData.EntityType1,
            new GetVisitListByCurrentUserInput());

        vists.Items.Any().ShouldBeTrue();
    }
}
