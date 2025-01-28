using Shouldly;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Dignite.Cms.Sections;

public class SectionManager_Tests : CmsDomainTestBase
{
    private readonly CmsTestData testData;
    private readonly SectionManager sectionManager;
    private readonly ISectionRepository sectionRepository;

    public SectionManager_Tests()
    {
        sectionManager = GetRequiredService<SectionManager>();
        testData = GetRequiredService<CmsTestData>();
        sectionRepository = GetRequiredService<ISectionRepository>();
    }


    [Fact]
    public async Task CreateAsync_ShouldWorkProperly()
    {
        var section = await sectionManager.CreateAsync(
            SectionType.Single,
            "New Section",
            "new-section-name",
            false, true, "news", "news/index",
            null
        );

        section.Id.ShouldNotBe(Guid.Empty);
        var sectionFromDb = await sectionRepository.GetAsync(section.Id);

        sectionFromDb.Type.ShouldBe(section.Type);
        sectionFromDb.Name.ShouldBe(section.Name);
        sectionFromDb.DisplayName.ShouldBe(section.DisplayName);
        sectionFromDb.Route.ShouldBe(section.Route);
        sectionFromDb.Template.ShouldBe(section.Template);
    }

    [Fact]
    public async Task CreateAsync_ShouldThrowException_WhenNameAlreadyExists()
    {
        var exception = await Should.ThrowAsync<SectionNameAlreadyExistException>(
            async () => await sectionManager.CreateAsync(
                SectionType.Single,
                "New Section",
                testData.SingleSectionName,
                false,
                true,
                "/",
                "home",
                null
            ));

        exception.ShouldNotBeNull();
        exception.Code.ShouldBe(CmsErrorCodes.Sections.NameAlreadyExist);
    }

    [Fact]
    public async Task CreateAsync_ShouldThrowException_WhenRouteAlreadyExists()
    {
        var exception = await Should.ThrowAsync<SectionRouteAlreadyExistException>(
            async () => await sectionManager.CreateAsync(
                SectionType.Single,
                "New Section",
                "new-section-new-route",
                false,
                true,
                "/",
                "home",
                null
            ));

        exception.ShouldNotBeNull();
        exception.Code.ShouldBe(CmsErrorCodes.Sections.RouteAlreadyExist);
    }

    [Fact]
    public async Task CreateAsync_ShouldThrowException_WithDefaultButNotSingleType()
    {
        var exception = await Should.ThrowAsync<DefaultSectionMustBeSingleTypeException>(
            async () => await sectionManager.CreateAsync(
                SectionType.Channel,
                "Default Section",
                "default-section",
                true,
                true,
                "contact",
                "contact/index",
                null
            ));

        exception.ShouldNotBeNull();
        exception.Code.ShouldBe(CmsErrorCodes.Sections.DefaultSectionMustBeSingleType);
    }

    [Fact]
    public async Task CreateAsync_ShouldThrowException_WithSlugRoutingParameter()
    {
        var exception = await Should.ThrowAsync<MissingSlugRoutingParameterException>(
            async () => await sectionManager.CreateAsync(
                SectionType.Structure,
                "Section",
                "structure-section",
                false,
                true,
                "qa",
                "qa/entry",
                null
            ));

        exception.ShouldNotBeNull();
        exception.Code.ShouldBe(CmsErrorCodes.Sections.RouteMissingSlugRoutingParameter);
    }
}
