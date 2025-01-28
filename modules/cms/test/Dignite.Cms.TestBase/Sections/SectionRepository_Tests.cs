using Shouldly;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Modularity;
using Xunit;

namespace Dignite.Cms.Sections;


public abstract class SectionRepository_Tests<TStartupModule> : CmsTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{
    private readonly CmsTestData testData;
    private readonly ISectionRepository sectionRepository;

    protected SectionRepository_Tests()
    {
        testData = GetRequiredService<CmsTestData>();
        sectionRepository = GetRequiredService<ISectionRepository>();
    }


    [Fact]
    public async Task NameExistsAsync_ShouldReturnTrue_WithExistingName()
    {
        var result = await sectionRepository.NameExistsAsync(testData.SingleSectionName);

        result.ShouldBeTrue();
    }

    [Fact]
    public async Task NameExistsAsync_ShouldReturnFalse_WithNonExistingName()
    {
        var nonExistingName = "any-other-name";

        var result = await sectionRepository.NameExistsAsync(nonExistingName);

        result.ShouldBeFalse();
    }

    [Fact]
    public async Task RouteExistsAsync_ShouldReturnTrue_WithExistingRoute()
    {
        var result = await sectionRepository.RouteExistsAsync(testData.ChannelSectionRoute);

        result.ShouldBeTrue();
    }

    [Fact]
    public async Task HostExistsAsync_ShouldReturnFalse_WithNonExistingRoute()
    {
        var nonExistingRoute = "about";

        var result = await sectionRepository.RouteExistsAsync(nonExistingRoute);

        result.ShouldBeFalse();
    }

    [Fact]
    public async Task FindByNameAsync_ShouldWorkProperly_WithCorrectParameters()
    {
        var result = await sectionRepository.FindByNameAsync(testData.ChannelSectionName);

        result.ShouldNotBeNull();
        result.Id.ShouldBe(testData.ChannelSectionId);
        result.Name.ShouldBe(testData.ChannelSectionName);
    }

    [Fact]
    public async Task ShouldNotFindByNameAsync()
    {
        var nonExistingName = "absolutely-non-existing-name";
        var section = await sectionRepository.FindByNameAsync(nonExistingName);

        section.ShouldBeNull();
    }


    [Fact]
    public async Task GetDefaultAsync_ShouldWorkProperly_WithCorrectParameters()
    {
        var result = await sectionRepository.GetDefaultAsync();

        result.ShouldNotBeNull();
        result.Id.ShouldBe(testData.SingleSectionId);
        result.Name.ShouldBe(testData.SingleSectionName);
        result.EntryTypes.ShouldNotBeEmpty();
    }


    [Fact]
    public async Task GetPagedListAsync_ShouldWorkProperly_WithSiteId_WhileGetting10_WithoutSorting()
    {
        var result = await sectionRepository.GetListAsync();

        result.ShouldNotBeNull();
        result.ShouldNotBeEmpty();
        result.Count.ShouldBeGreaterThan(0);
    }

    [Fact]
    public async Task GetPagedListAsync_ShouldWorkProperly_WithSiteId_WhileGetting1_WithoutSorting()
    {
        var result = await sectionRepository.GetListAsync(maxResultCount: 1);

        result.ShouldNotBeNull();
        result.ShouldNotBeEmpty();
        result.Count.ShouldBe(1);
    }

    [Fact]
    public async Task GetPagedListAsync_ShouldWorkProperly_WithSiteId_WhileGetting1InPage2_WithoutSorting()
    {
        var result = await sectionRepository.GetListAsync(skipCount: 1, maxResultCount: 1);

        result.ShouldNotBeNull();
        result.ShouldNotBeEmpty();
        result.Count.ShouldBe(1);
    }

    [Fact]
    public async Task GetPagedListAsync_ShouldWorkProperly_WithSiteId_WhileGetting10_WithSortingByName()
    {
        var result = await sectionRepository.GetListAsync(sorting: $"{nameof(Section.Name)} asc");

        result.ShouldNotBeNull();
        result.ShouldNotBeEmpty();
        result.Count.ShouldBeGreaterThan(0);
    }
}
