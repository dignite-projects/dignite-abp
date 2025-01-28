using Dignite.Cms.Admin.Sections;
using Shouldly;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Xunit;

namespace Dignite.Cms.Sections;

public class SectionAdminAppService_Tests : CmsApplicationTestBase
{
    private readonly ISectionAdminAppService sectionAdminAppService;
    private readonly CmsTestData testData;

    public SectionAdminAppService_Tests()
    {
        sectionAdminAppService = GetRequiredService<ISectionAdminAppService>();
        testData = GetRequiredService<CmsTestData>();
    }


    [Fact]
    public async Task GetAsync()
    {
        var section = await sectionAdminAppService.GetAsync(testData.ChannelSectionId);

        section.Name.ShouldBe(testData.ChannelSectionName);
        section.EntryTypes.ShouldNotBeEmpty();
    }

    [Fact]
    public async Task GetListAsync()
    {
        var sections = await sectionAdminAppService.GetListAsync(new GetSectionsInput() { 
        });

        sections.TotalCount.ShouldBeGreaterThan(0);
        sections.Items.Any(x => x.Name == testData.SingleSectionName).ShouldBeTrue();
    }

    [Fact]
    public async Task NameExistsAsync()
    {
        var result = await sectionAdminAppService.NameExistsAsync(new SectionNameExistsInput { 
            Name=testData.SingleSectionName
        });

        result.ShouldBe(true);
    }

    [Fact]
    public async Task RouteExistsAsync()
    {
        var result = await sectionAdminAppService.RouteExistsAsync(
            new SectionRouteExistsInput
            {
                Route = testData.ChannelSectionRoute
            }
            );

        result.ShouldBe(true);
    }

    [Fact]
    public async Task CreateAsync_ShouldWork()
    {
        var name = "new-section";
        var route = "product/{slug}";
        var section = await sectionAdminAppService.CreateAsync(new CreateSectionInput
        {
            Name = name,
            DisplayName = "New section",
            Route = route,
            IsActive = true,
            IsDefault = false,
            Type = SectionType.Channel,
            Template = "product/entry"
        });

        section.ShouldNotBeNull();
        section.Name.ShouldBe(name);
        section.Route.ShouldBe(route);
    }

    [Fact]
    public async Task CreateAsync_ShouldThrow_WithExistName()
    {
        await Should.ThrowAsync<SectionNameAlreadyExistException>(
            async () =>
                await sectionAdminAppService.CreateAsync(new CreateSectionInput
                {
                    Name = testData.ChannelSectionName,
                    DisplayName = "New section",
                    Route = "product",
                    IsActive = true,
                    IsDefault = false,
                    Type = SectionType.Channel,
                    Template = "product/index"
                })
            );
    }

    [Fact]
    public async Task CreateAsync_ShouldThrow_WithExistRoute()
    {
        await Should.ThrowAsync<SectionRouteAlreadyExistException>(
            async () =>
                await sectionAdminAppService.CreateAsync(new CreateSectionInput
                {
                    Name = "new-section-a",
                    DisplayName = "New section",
                    Route = testData.ChannelSectionRoute,
                    IsActive = true,
                    IsDefault = false,
                    Type = SectionType.Channel,
                    Template = "product/index"
                })
            );
    }

    [Fact]
    public async Task UpdateAsync_ShouldWork()
    {
        var newDisplayName = "New Display Name";
        var section = await sectionAdminAppService.UpdateAsync(
            testData.ChannelSectionId, 
            new UpdateSectionInput
            {
                DisplayName = newDisplayName,
                Name = testData.ChannelSectionName,
                Route = testData.ChannelSectionRoute,
                IsActive = true,
                IsDefault = false,
                Type = SectionType.Channel,
                Template = "blog/entry"
            });

        var updatedSection = await sectionAdminAppService.GetAsync(testData.ChannelSectionId);

        updatedSection.DisplayName.ShouldBe(newDisplayName);
    }

    [Fact]
    public async Task DeleteAsync_ShouldWork()
    {
        await sectionAdminAppService.DeleteAsync(testData.ChannelSectionId);

        await Should.ThrowAsync<EntityNotFoundException>(
            async () =>
                await sectionAdminAppService.GetAsync(testData.ChannelSectionId)
        );
    }
}
