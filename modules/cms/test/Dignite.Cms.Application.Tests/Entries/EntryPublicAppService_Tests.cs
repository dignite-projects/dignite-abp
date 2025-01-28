using Dignite.Abp.Data;
using Dignite.Cms.Public.Entries;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace Dignite.Cms.Entries;

public class EntryPublicAppService_Tests : CmsApplicationTestBase
{
    private readonly IEntryPublicAppService entryPublicAppService;
    private readonly CmsTestData testData;

    public EntryPublicAppService_Tests()
    {
        entryPublicAppService = GetRequiredService<IEntryPublicAppService>();
        testData = GetRequiredService<CmsTestData>();
    }

    [Fact]
    public async Task GetAsync()
    {
        var result = await entryPublicAppService.GetAsync(testData.SingleSection_EntryId);

        result.ShouldNotBeNull();
        result.Id.ShouldBe(testData.SingleSection_EntryId);
    }

    [Fact]
    public async Task FindBySlugAsync_ShouldWorkProperly_WithExistingSlug()
    {
        var result = await entryPublicAppService.FindBySlugAsync(
            new FindBySlugInput
            {
                SectionId = testData.ChannelSectionId,
                Culture = testData.EntryDefaultCulture,
                Slug = testData.ChannelSection_Entry1Slug
            }
            );

        result.ShouldNotBeNull();
        result.Id.ShouldBe(testData.ChannelSection_Entry1Id);
    }

    [Fact]
    public async Task GetListAsync()
    {
        var entries = await entryPublicAppService.GetListAsync(
            new GetEntriesInput { 
                SectionId = testData.ChannelSectionId,
                Culture = testData.EntryDefaultCulture
            }
            );

        entries.TotalCount.ShouldBeGreaterThan(0);
        entries.Items.Any(x => x.Slug == testData.ChannelSection_Entry1Slug).ShouldBeTrue();
    }

    [Fact]
    public async Task GetListAsync_ShouldFilter_ByFields()
    {
        var parameters = new List<QueryingByField>
        {
            new QueryingByField(testData.SelectFieldName,testData.SelectFieldItem2Value)
        };

        var entries = await entryPublicAppService.GetListAsync(
            new GetEntriesInput
            {
                SectionId = testData.ChannelSectionId,
                Culture = testData.EntryDefaultCulture,
                QueryingByFieldsJson = JsonSerializer.Serialize(parameters)
            }
            );

        entries.TotalCount.ShouldBeGreaterThan(0);
        entries.Items.Any(x => x.Slug == testData.ChannelSection_Entry1Slug).ShouldBeTrue();
    }

    [Fact]
    public async Task GetListAsync_ShouldWorkProperly_WithIds()
    {
        var entries = await entryPublicAppService.GetListAsync(
            new GetEntriesInput
            {
                SectionId = testData.ChannelSectionId,
                Culture = testData.EntryDefaultCulture,
                EntryIds = new Guid[] { 
                    testData.ChannelSection_Entry1Id, 
                    testData.ChannelSection_Entry2Id 
                }
            }
            );

        entries.TotalCount.ShouldBeGreaterThan(0);
        entries.Items.Any(x => x.Slug == testData.ChannelSection_Entry1Slug).ShouldBeTrue();
    }
}
