using System.Threading.Tasks;
using Dignite.Cms.Public.Fields;
using Shouldly;
using Xunit;

namespace Dignite.Cms.Fields;

public class FieldPublicAppService_Tests : CmsApplicationTestBase
{
    private readonly IFieldPublicAppService fieldPublicAppService;
    private readonly CmsTestData testData;

    public FieldPublicAppService_Tests()
    {
        fieldPublicAppService = GetRequiredService<IFieldPublicAppService>();
        testData = GetRequiredService<CmsTestData>();
    }

    [Fact]
    public async Task GetAsync_ShouldWorkProperly_WithExistingName()
    {
        var result = await fieldPublicAppService.FindByNameAsync(testData.TextboxFieldName);

        result.ShouldNotBeNull();
        result.Id.ShouldBe(testData.TextboxFieldId);
    }

}
