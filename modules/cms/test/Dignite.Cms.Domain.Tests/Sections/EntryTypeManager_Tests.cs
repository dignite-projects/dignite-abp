using Shouldly;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Dignite.Cms.Sections;

public class EntryTypeManager_Tests : CmsDomainTestBase
{
    private readonly CmsTestData testData;
    private readonly EntryTypeManager entryTypeManager;
    private readonly IEntryTypeRepository entryTypeRepository;

    public EntryTypeManager_Tests()
    {
        entryTypeManager = GetRequiredService<EntryTypeManager>();
        testData = GetRequiredService<CmsTestData>();
        entryTypeRepository = GetRequiredService<IEntryTypeRepository>();
    }


    [Fact]
    public async Task CreateAsync_ShouldWorkProperly()
    {
        var entryType = await entryTypeManager.CreateAsync(
            testData.SingleSectionId,
            "New Entry Type",
            "new-entry-type-name",
            new List<EntryFieldTab> {
                    new EntryFieldTab(
                        "Entry Field Tab",
                        new List<EntryField>{
                            new EntryField(
                                testData.TextboxFieldId,
                                "Textbox Field",
                                true,
                                true,
                                true
                                ),
                            new EntryField(
                                testData.SelectFieldId,
                                "Select Field",
                                true,
                                true,
                                true
                                )
                        })
                    }
        );
        await entryTypeRepository.InsertAsync( entryType );

        entryType.Id.ShouldNotBe(Guid.Empty);
        var entryTypeFromDb = await entryTypeRepository.GetAsync(entryType.Id);

        entryTypeFromDb.SectionId.ShouldBe(entryType.SectionId);
        entryTypeFromDb.Name.ShouldBe(entryType.Name);
        entryTypeFromDb.DisplayName.ShouldBe(entryType.DisplayName);
        entryTypeFromDb.FieldTabs.ShouldNotBeEmpty();
    }


    [Fact]
    public async Task CreateAsync_ShouldThrowException_WithNonExistingName()
    {
        var exception = await Should.ThrowAsync<EntryTypeNameAlreadyExistException>(
            async () => await entryTypeManager.CreateAsync(
            testData.SingleSectionId,
            "New Entry Type",
            testData.SingleSectionEntryTypeName,
            new List<EntryFieldTab> {
                    new EntryFieldTab(
                        "Entry Field Tab",
                        new List<EntryField>{
                            new EntryField(
                                testData.TextboxFieldId,
                                "Textbox Field",
                                true,
                                true,
                                true
                                ),
                            new EntryField(
                                testData.SelectFieldId,
                                "Select Field",
                                true,
                                true,
                                true
                                )
                        })
                    }
        ));

        exception.ShouldNotBeNull();
        exception.Code.ShouldBe(CmsErrorCodes.Sections.NameAlreadyExist);
    }
}
