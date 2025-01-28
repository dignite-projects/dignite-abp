using Shouldly;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Modularity;
using Xunit;

namespace Dignite.Cms.Entries;


public abstract class EntryRepository_Tests<TStartupModule> : CmsTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{
    private readonly CmsTestData testData;
    private readonly IEntryRepository entryRepository;

    protected EntryRepository_Tests()
    {
        testData = GetRequiredService<CmsTestData>();
        entryRepository = GetRequiredService<IEntryRepository>();
    }


    [Fact]
    public async Task SlugExistsAsync_ShouldReturnTrue_WithExistingSlug()
    {
        var result = await entryRepository.SlugExistsAsync(
            testData.EntryDefaultCulture,
            testData.ChannelSectionId,
            testData.ChannelSection_Entry1Slug);

        result.ShouldBeTrue();
    }

    [Fact]
    public async Task SlugExistsAsync_ShouldReturnFalse_WithNonExistingSlug()
    {
        var nonExistingSlug = "any-other-slug";

        var result = await entryRepository.SlugExistsAsync(
            testData.EntryDefaultCulture,
            testData.ChannelSectionId,
            nonExistingSlug);

        result.ShouldBeFalse();
    }

    [Fact]
    public async Task CultureExistsAsync_ShouldReturnTrue_WithExistingCulture()
    {
        var result = await entryRepository.CultureExistWithSingleSectionAsync(
            testData.EntryDefaultCulture,
            testData.SingleSectionId,
            testData.SingleSectionEntryTypeId);

        result.ShouldBeTrue();
    }

    [Fact]
    public async Task CultureExistsAsync_ShouldReturnFalse_WithNonExistingCulture()
    {
        var nonExistingCulture = "ja-Jp";

        var result = await entryRepository.CultureExistWithSingleSectionAsync(
            nonExistingCulture,
            testData.SingleSectionId,
            testData.SingleSectionEntryTypeId);

        result.ShouldBeFalse();
    }

    [Fact]
    public async Task FindBySlugAsync_ShouldWorkProperly_WithCorrectParameters()
    {
        var result = await entryRepository.FindBySlugAsync(
            testData.EntryDefaultCulture,
            testData.ChannelSectionId,
            testData.ChannelSection_Entry1Slug);

        result.ShouldNotBeNull();
        result.Id.ShouldBe(testData.ChannelSection_Entry1Id);
        result.Slug.ShouldBe(testData.ChannelSection_Entry1Slug);
    }

    [Fact]
    public async Task ShouldNotFindBySlugAsync()
    {
        var nonExistingSlug = "absolutely-non-existing-slug";
        var entry = await entryRepository.FindBySlugAsync(
                testData.EntryDefaultCulture,
                testData.ChannelSectionId,
                nonExistingSlug
                );

        entry.ShouldBeNull();
    }

    [Fact]
    public async Task GetPagedListAsync_ShouldWorkProperly_WithSection_WhileGetting10_WithoutSorting()
    {
        var result = await entryRepository.GetListAsync(
            testData.EntryDefaultCulture,
            testData.ChannelSectionId);

        result.ShouldNotBeNull();
        result.ShouldNotBeEmpty();
        result.Count.ShouldBe(2);
    }

    [Fact]
    public async Task GetPagedListAsync_ShouldWorkProperly_WithSection_WhileGetting1_WithoutSorting()
    {
        var result = await entryRepository.GetListAsync(
            testData.EntryDefaultCulture,
            testData.ChannelSectionId, 
            maxResultCount: 1);

        result.ShouldNotBeNull();
        result.ShouldNotBeEmpty();
        result.Count.ShouldBe(1);
    }

    [Fact]
    public async Task GetPagedListAsync_ShouldWorkProperly_WithSection_WhileGetting1InPage2_WithoutSorting()
    {
        var result = await entryRepository.GetListAsync(
            testData.EntryDefaultCulture,
            testData.ChannelSectionId, 
            skipCount: 1, 
            maxResultCount: 1);

        result.ShouldNotBeNull();
        result.ShouldNotBeEmpty();
        result.Count.ShouldBe(1);
    }

    [Fact]
    public async Task GetPagedListAsync_ShouldWorkProperly_WithSection_WhileGetting10_WithSortingByName()
    {
        var result = await entryRepository.GetListAsync(
            testData.EntryDefaultCulture,
            testData.ChannelSectionId,
            sorting: $"{nameof(Entry.PublishTime)} asc");

        result.ShouldNotBeNull();
        result.ShouldNotBeEmpty();
        result.Count.ShouldBe(2);
    }

    [Fact]
    public async Task GetListAsync_ShouldWorkProperly_WithIds()
    {
        var result = await entryRepository.GetListAsync(
            testData.ChannelSectionId,
            new List<Guid> {
                testData.ChannelSection_Entry1Id,
                testData.ChannelSection_Entry2Id
                }
            );

        result.ShouldNotBeNull();
        result.ShouldNotBeEmpty();
        result.Count.ShouldBe(2);
    }

    [Fact]
    public async Task GetVisionListAsync_ShouldWorkProperly()
    {
        var result = await entryRepository.GetVisionListAsync(
            testData.ChannelSection_Entry2Id
            );

        result.ShouldNotBeNull();
        result.ShouldNotBeEmpty();
        result.Count.ShouldBe(2);
    }


    [Fact]
    public async Task FindNextAsync_ShouldWorkProperly_WithCorrectParameters()
    {
        var result = await entryRepository.FindNextAsync(
            testData.ChannelSection_Entry1Id);

        result.ShouldNotBeNull();
        result.Id.ShouldBe(testData.ChannelSection_Entry2Id);
        result.Slug.ShouldBe(testData.ChannelSection_Entry2Slug);
    }

    [Fact]
    public async Task GetMaxOrderAsync_ShouldWorkProperly()
    {
        var result = await entryRepository.GetMaxOrderAsync(
            testData.EntryDefaultCulture,
            testData.ChannelSectionId,
            null
            );

        result.ShouldBe(2);
    }
}
