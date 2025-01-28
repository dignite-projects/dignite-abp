using Shouldly;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Modularity;
using Xunit;

namespace Dignite.Cms.Fields;


public abstract class FieldRepository_Tests<TStartupModule> : CmsTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{
    private readonly CmsTestData testData;
    private readonly IFieldRepository fieldRepository;

    protected FieldRepository_Tests()
    {
        testData = GetRequiredService<CmsTestData>();
        fieldRepository = GetRequiredService<IFieldRepository>();
    }


    [Fact]
    public async Task NameExistsAsync_ShouldReturnTrue_WithExistingName()
    {
        var result = await fieldRepository.NameExistsAsync(testData.TextboxFieldName);

        result.ShouldBeTrue();
    }

    [Fact]
    public async Task NameExistsAsync_ShouldReturnFalse_WithNonExistingName()
    {
        var nonExistingName = "any-other-name";

        var result = await fieldRepository.NameExistsAsync(nonExistingName);

        result.ShouldBeFalse();
    }

    [Fact]
    public async Task GetByNameAsync_ShouldWorkProperly_WithCorrectParameters()
    {
        var field = await fieldRepository.FindByNameAsync(testData.TextboxFieldName);

        field.ShouldNotBeNull();
        field.Id.ShouldBe(testData.TextboxFieldId);
        field.Name.ShouldBe(testData.TextboxFieldName);
    }

    [Fact]
    public async Task ShouldNotFindByNameAsync()
    {
        var nonExistingName = "absolutely-non-existing-name";
        var field =  await fieldRepository.FindByNameAsync(nonExistingName);

        field.ShouldBeNull();
    }

    [Fact]
    public async Task GetPagedListAsync_ShouldWorkProperly_WhileGetting10_WithoutSorting()
    {
        var result = await fieldRepository.GetListAsync(null, null);

        result.ShouldNotBeNull();
        result.ShouldNotBeEmpty();
        result.Count.ShouldBe(2);
    }

    [Fact]
    public async Task GetPagedListAsync_ShouldWorkProperly_WhileGetting1_WithoutSorting()
    {
        var result = await fieldRepository.GetListAsync(null, null, maxResultCount: 1);

        result.ShouldNotBeNull();
        result.ShouldNotBeEmpty();
        result.Count.ShouldBe(1);
    }

    [Fact]
    public async Task GetPagedListAsync_ShouldWorkProperly_WhileGetting1InPage2_WithoutSorting()
    {
        var result = await fieldRepository.GetListAsync(null, null, skipCount: 1, maxResultCount: 1);

        result.ShouldNotBeNull();
        result.ShouldNotBeEmpty();
        result.Count.ShouldBe(1);
    }

    [Fact]
    public async Task GetPagedListAsync_ShouldWorkProperly_WhileGetting10_WithSortingByName()
    {
        var result = await fieldRepository.GetListAsync(null, null, sorting: $"{nameof(Field.Name)} asc");

        result.ShouldNotBeNull();
        result.ShouldNotBeEmpty();
        result.Count.ShouldBe(2);
    }

    [Fact]
    public async Task GetPagedListAsync_ShouldWorkProperly_WithGroupId_WhileGetting1_WithoutSorting()
    {
        var result = await fieldRepository.GetListAsync(groupId: testData.FieldGroupId, null, skipCount: 1, maxResultCount: 1);

        result.ShouldNotBeNull();
        result.ShouldNotBeEmpty();
        result.Count.ShouldBe(1);
    }


    [Fact]
    public async Task GetListAsync_ShouldWorkProperly_WithIds()
    {
        var result = await fieldRepository.GetListAsync(
            new List<Guid> {
                testData.SelectFieldId,
                testData.TextboxFieldId
                }
            );

        result.ShouldNotBeNull();
        result.ShouldNotBeEmpty();
        result.Count.ShouldBe(2);
    }

}
