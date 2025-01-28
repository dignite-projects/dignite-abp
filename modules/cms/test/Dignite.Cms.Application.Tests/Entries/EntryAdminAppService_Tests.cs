using Dignite.Cms.Admin.Entries;
using Shouldly;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Timing;
using Xunit;

namespace Dignite.Cms.Entries;

public class EntryAdminAppService_Tests : CmsApplicationTestBase
{
    private readonly IClock _clock;
    private readonly IEntryAdminAppService entryAdminAppService;
    private readonly CmsTestData testData;

    public EntryAdminAppService_Tests()
    {
        _clock = GetRequiredService<IClock>();
        entryAdminAppService = GetRequiredService<IEntryAdminAppService>();
        testData = GetRequiredService<CmsTestData>();
    }


    [Fact]
    public async Task GetAsync()
    {
        var entry = await entryAdminAppService.GetAsync(testData.SingleSection_EntryId);

        entry.Slug.ShouldBe(testData.SingleSection_EntrySlug);
    }

    [Fact]
    public async Task GetAllVersionsAsync()
    {
        var entries = await entryAdminAppService.GetAllVersionsAsync(testData.ChannelSection_Entry2Id);

        entries.Items.Count.ShouldBeGreaterThan(0);
    }

    [Fact]
    public async Task GetListAsync()
    {
        var Entries = await entryAdminAppService.GetListAsync(
            new GetEntriesInput
            {
                SectionId=testData.ChannelSectionId,
                Culture=testData.EntryDefaultCulture
            }
            );

        Entries.TotalCount.ShouldBeGreaterThan(0);
    }

    [Fact]
    public async Task NameExistsAsync()
    {
        var result = await entryAdminAppService.SlugExistsAsync(
            new SlugExistsInput { 
                SectionId = testData.ChannelSectionId,
                Culture=testData.EntryDefaultCulture,
                Slug= testData.ChannelSection_Entry1Slug
            }
            );

        result.ShouldBe(true);
    }

    [Fact]
    public async Task CultureExistWithSingleSectionAsync()
    {
        var result = await entryAdminAppService.CultureExistWithSingleSectionAsync(
            new CultureExistWithSingleSectionInput
            {
                SectionId = testData.SingleSectionId,
                Culture = testData.EntryDefaultCulture,
                EntryTypeId = testData.SingleSectionEntryTypeId
            }
            );

        result.ShouldBe(true);
    }

    [Fact]
    public async Task CreateAsync_ShouldWork()
    {
        var extraProperties = new ExtraPropertyDictionary
        {
            { testData.TextboxFieldName, "Tanaka" },
            {
                testData.SelectFieldName,
                new List<string>
                {
                    testData.SelectFieldItem1Value,
                    testData.SelectFieldItem2Value,
                    testData.SelectFieldItem3Value
                }
            }
        };
        var newEntrySlug = "new-entry-slug-by-app-service";
        var entry = await entryAdminAppService.CreateAsync(
            new CreateEntryInput
            {
                EntryTypeId = testData.ChannelSectionEntryTypeId,
                Culture = testData.EntryDefaultCulture,
                Slug = newEntrySlug,
                Title = "New Entry",
                PublishTime = _clock.Now,
                ExtraProperties = extraProperties
            });

        entry.ShouldNotBeNull();
        entry.Slug.ShouldBe(newEntrySlug);
    }

    [Fact]
    public async Task CreateAsync_ShouldThrowException_WhenSlugAlreadyExists()
    {
        var extraProperties = new ExtraPropertyDictionary
        {
            { testData.TextboxFieldName, "Tanaka" },
            {
                testData.SelectFieldName,
                new List<string>
                {
                    testData.SelectFieldItem1Value,
                    testData.SelectFieldItem2Value,
                    testData.SelectFieldItem3Value
                }
            }
        };

        var exception = await Should.ThrowAsync<EntrySlugAlreadyExistException>(
            async () => await entryAdminAppService.CreateAsync(
                new CreateEntryInput
                {
                    EntryTypeId = testData.ChannelSectionEntryTypeId,
                    Culture = testData.EntryDefaultCulture,
                    Slug = testData.ChannelSection_Entry1Slug,
                    Title = "New Entry",
                    PublishTime = _clock.Now,
                    ExtraProperties = extraProperties
                }
                ));
    }

    [Fact]
    public async Task CreateAsync_ShouldThrowException_WhenCultureAlreadyExists()
    {
        var extraProperties = new ExtraPropertyDictionary
        {
            { testData.TextboxFieldName, "Tanaka" },
            {
                testData.SelectFieldName,
                new List<string>
                {
                    testData.SelectFieldItem1Value,
                    testData.SelectFieldItem2Value,
                    testData.SelectFieldItem3Value
                }
            }
        };

        var exception = await Should.ThrowAsync<EntryCultureAlreadyExistException>(
            async () => await entryAdminAppService.CreateAsync(
                new CreateEntryInput
                {
                    EntryTypeId = testData.SingleSectionEntryTypeId,
                    Culture = testData.EntryDefaultCulture,
                    Slug = testData.SingleSection_EntrySlug,
                    Title = "New Entry",
                    PublishTime = _clock.Now,
                    ExtraProperties = extraProperties
                }
                ));
    }


    [Fact]
    public async Task CreateAsync_ShouldThrowException_WhenInformationInconsistent()
    {
        var extraProperties = new ExtraPropertyDictionary
        {
            { testData.TextboxFieldName, "Tanaka" },
            {
                testData.SelectFieldName,
                new List<string>
                {
                    testData.SelectFieldItem1Value,
                    testData.SelectFieldItem2Value,
                    testData.SelectFieldItem3Value
                }
            }
        };

        var exception = await Should.ThrowAsync<EntryInformationInconsistentException>(
            async () => await entryAdminAppService.CreateAsync(
                new CreateEntryInput
                {
                    EntryTypeId = testData.ChannelSectionEntryTypeId,
                    Culture = "ja",
                    Slug = testData.ChannelSection_Entry1Slug,
                    Title = "New Entry",
                    PublishTime = _clock.Now,
                    InitialVersionId = testData.ChannelSection_Entry1Id,
                    ExtraProperties = extraProperties
                }
                ));
    }


    [Fact]
    public async Task UpdateAsync_ShouldWork()
    {
        var extraProperties = new ExtraPropertyDictionary
        {
            { testData.TextboxFieldName, "Tanaka" },
            {
                testData.SelectFieldName,
                new List<string>
                {
                    testData.SelectFieldItem1Value,
                    testData.SelectFieldItem2Value,
                    testData.SelectFieldItem3Value
                }
            }
        };
        var newTitle = "New Entry Title";
        var entry = await entryAdminAppService.UpdateAsync(testData.ChannelSection_Entry2Id, 
            new UpdateEntryInput
            {
                EntryTypeId = testData.ChannelSectionEntryTypeId,
                Culture = testData.EntryDefaultCulture,
                Slug = testData.ChannelSection_Entry2Slug,
                Title = newTitle,
                PublishTime = _clock.Now,
                ExtraProperties = extraProperties,
            });

        var updatedentry = await entryAdminAppService.GetAsync(testData.ChannelSection_Entry2Id);

        updatedentry.Title.ShouldBe(newTitle);
    }

    [Fact]
    public async Task ActivateAsync_ShouldWork()
    {
        await entryAdminAppService.ActivateAsync(testData.ChannelSection_Entry2VisionEntryId);

        var updatedentry = await entryAdminAppService.GetAsync(testData.ChannelSection_Entry2VisionEntryId);

        updatedentry.IsActivatedVersion.ShouldBeTrue();
    }


    [Fact]
    public async Task MoveAsync_ShouldWork()
    {
        await entryAdminAppService.MoveAsync(
            testData.StructureSection_Entry2Id,
            new MoveEntryInput
            {
                ParentId = testData.StructureSection_Entry1Id,
                Order=1
            }
            );

        var updatedentry = await entryAdminAppService.GetAsync(testData.StructureSection_Entry2Id);

        updatedentry.ParentId.ShouldBe(testData.StructureSection_Entry1Id);
    }



    [Fact]
    public async Task DeleteAsync_ShouldWork()
    {
        await entryAdminAppService.DeleteAsync(testData.StructureSection_Entry2Id);

        await Should.ThrowAsync<EntityNotFoundException>(
            async () =>
                await entryAdminAppService.GetAsync(testData.StructureSection_Entry2Id)
        );
    }
}
