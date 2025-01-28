using Shouldly;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Modularity;
using Xunit;

namespace Dignite.Cms.Sections;


public abstract class EntryTypeRepository_Tests<TStartupModule> : CmsTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{
    private readonly CmsTestData testData;
    private readonly IEntryTypeRepository entryTypeRepository;

    protected EntryTypeRepository_Tests()
    {
        testData = GetRequiredService<CmsTestData>();
        entryTypeRepository = GetRequiredService<IEntryTypeRepository>();
    }


    [Fact]
    public async Task NameExistsAsync_ShouldReturnTrue_WithExistingName()
    {
        var result = await entryTypeRepository.NameExistsAsync(testData.SingleSectionId, testData.SingleSectionEntryTypeName);

        result.ShouldBeTrue();
    }

    [Fact]
    public async Task NameExistsAsync_ShouldReturnFalse_WithNonExistingName()
    {
        var nonExistingName = "any-other-name";

        var result = await entryTypeRepository.NameExistsAsync(testData.SingleSectionId, nonExistingName);

        result.ShouldBeFalse();
    }

}
