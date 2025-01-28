using Dignite.Cms.Public.Sections;
using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace Dignite.Cms.Sections;

public class SectionPublicAppService_Tests : CmsApplicationTestBase
{
    private readonly ISectionPublicAppService sectionPublicAppService;
    private readonly CmsTestData testData;

    public SectionPublicAppService_Tests()
    {
        sectionPublicAppService = GetRequiredService<ISectionPublicAppService>();
        testData = GetRequiredService<CmsTestData>();
    }


    [Fact]
    public async Task GetListAsync()
    {
        var entries = await sectionPublicAppService.GetListAsync(
            new GetSectionsInput
            {
            }
            );

        entries.Items.Count.ShouldBeGreaterThan(0);
    }

    [Fact]
    public async Task GetAsync()
    {
        var section = await sectionPublicAppService.GetAsync(testData.ChannelSectionId);

        section.Name.ShouldBe(testData.ChannelSectionName);
        section.EntryTypes.ShouldNotBeEmpty();
    }

    [Fact]
    public async Task FindByNameAsync_ShouldWorkProperly_WithExistingName()
    {
        var section = await sectionPublicAppService.FindByNameAsync(testData.ChannelSectionName);

        section.Name.ShouldBe(testData.ChannelSectionName);
        section.EntryTypes.ShouldNotBeEmpty();
    }

    [Fact]
    public async Task FindByEntryPathAsync_ShouldWorkProperly_WithEntryPath()
    {
        var section = await sectionPublicAppService.FindByEntityPathAsync( "blog/a-post-slug");

        section.Name.ShouldBe(testData.ChannelSectionName);
        section.EntryTypes.ShouldNotBeEmpty();
    }

    [Fact]
    public async Task GetDefaultAsync()
    {
        var section = await sectionPublicAppService.GetDefaultAsync();

        section.Name.ShouldBe(testData.SingleSectionName);
        section.EntryTypes.ShouldNotBeEmpty();
    }
}
