using Shouldly;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.Timing;
using Xunit;

namespace Dignite.Cms.Entries;

public class EntryManager_Tests : CmsDomainTestBase
{
    private readonly IClock _clock;
    private readonly CmsTestData testData;
    private readonly EntryManager entryManager;
    private readonly IEntryRepository entryRepository;

    public EntryManager_Tests()
    {
        _clock = GetRequiredService<IClock>();
        entryManager = GetRequiredService<EntryManager>();
        testData = GetRequiredService<CmsTestData>();
        entryRepository = GetRequiredService<IEntryRepository>();
    }


    [Fact]
    public async Task CreateAsync_WithDraftStatus()
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

        var entry = await entryManager.CreateAsync(
            testData.ChannelSectionEntryTypeId,
            testData.EntryDefaultCulture,
            "the-third-blog-post-draft",
            _clock.Now,
            EntryStatus.Draft,
            null,
            extraProperties,
            null,
            "",
            null
        );

        var entryFromDb = await entryRepository.GetAsync(entry.Id);

        entryFromDb.SectionId.ShouldBe(entry.SectionId);
        entryFromDb.Slug.ShouldBe(entry.Slug);
        entryFromDb.Culture.ShouldBe(entry.Culture);
        entryFromDb.EntryTypeId.ShouldBe(entry.EntryTypeId);
        entryFromDb.Status.ShouldBe(EntryStatus.Draft);
    }

    [Fact]
    public async Task CreateAsync_WithPublishedStatus()
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

        var entry = await entryManager.CreateAsync(
            testData.ChannelSectionEntryTypeId,
            testData.EntryDefaultCulture,
            "the-third-blog-post-published",
            _clock.Now,
            EntryStatus.Published,
            null,
            extraProperties,
            null,
            "",
            null
        );

        var entryFromDb = await entryRepository.GetAsync(entry.Id);

        entryFromDb.SectionId.ShouldBe(entry.SectionId);
        entryFromDb.Slug.ShouldBe(entry.Slug);
        entryFromDb.Culture.ShouldBe(entry.Culture);
        entryFromDb.EntryTypeId.ShouldBe(entry.EntryTypeId);
        entryFromDb.Status.ShouldBe(EntryStatus.Published);
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
            async () => await entryManager.CreateAsync(
                testData.ChannelSectionEntryTypeId,
                testData.EntryDefaultCulture,
                testData.ChannelSection_Entry1Slug,
                _clock.Now,
                EntryStatus.Published,
                null,
                extraProperties,
                null,
                "",
                null
            ));

        exception.ShouldNotBeNull();
        exception.Code.ShouldBe(CmsErrorCodes.Entries.SlugAlreadyExist);
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
            async () => await entryManager.CreateAsync(
                testData.SingleSectionEntryTypeId,
                testData.EntryDefaultCulture,
                "mew-blog-post-en",
                _clock.Now,
                EntryStatus.Published,
                null,
                extraProperties,
                null,
                "",
                null
            ));

        exception.ShouldNotBeNull();
        exception.Code.ShouldBe(CmsErrorCodes.Entries.CultureAlreadyExist);
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
            async () => await entryManager.CreateAsync(
                testData.ChannelSectionEntryTypeId,
                "ja",   //The initial version of culture is en
                "new-blog-post-ja",
                _clock.Now,
                EntryStatus.Published,
                null,
                extraProperties,
                testData.ChannelSection_Entry1Id,
                "",
                null
            ));

        exception.ShouldNotBeNull();
        exception.Code.ShouldBe(CmsErrorCodes.Entries.InformationInconsistent);
    }
}
