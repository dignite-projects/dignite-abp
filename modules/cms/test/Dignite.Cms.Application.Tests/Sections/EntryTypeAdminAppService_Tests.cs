using Dignite.Cms.Admin.Sections;
using Shouldly;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Xunit;

namespace Dignite.Cms.Sections;

public class EntryTypeAdminAppService_Tests : CmsApplicationTestBase
{
    private readonly IEntryTypeAdminAppService entryTypeAdminAppService;
    private readonly CmsTestData testData;

    public EntryTypeAdminAppService_Tests()
    {
        entryTypeAdminAppService = GetRequiredService<IEntryTypeAdminAppService>();
        testData = GetRequiredService<CmsTestData>();
    }


    [Fact]
    public async Task GetAsync()
    {
        var entryType = await entryTypeAdminAppService.GetAsync(testData.ChannelSectionEntryTypeId);

        entryType.Name.ShouldBe(testData.ChannelSectionEntryTypeName);
    }

    [Fact]
    public async Task NameExistsAsync()
    {
        var result = await entryTypeAdminAppService.NameExistsAsync(
            new EntryTypeNameExistsInput(
                testData.ChannelSectionId,
                testData.ChannelSectionEntryTypeName)
            );

        result.ShouldBe(true);
    }

    [Fact]
    public async Task CreateAsync_ShouldWork()
    {
        var entryTypeName = "new-entry-type";
        var input = new CreateEntryTypeInput(testData.ChannelSectionId);
        input.DisplayName = "New Entry Type";
        input.Name = entryTypeName;
        input.FieldTabs = new List<EntryFieldTabInput> { 
            new EntryFieldTabInput{ 
                Name = "Default",
                Fields = new List<EntryFieldInput>
                {
                    new EntryFieldInput
                    {
                        FieldId=testData.TextboxFieldId,
                        ShowOnList = true,
                        DisplayName = "Textbox Field",
                        Required = true,
                    },
                    new EntryFieldInput
                    {
                        FieldId=testData.SelectFieldId,
                        ShowOnList = true,
                        DisplayName = "Select Field",
                        Required = true,
                    }
                }
            }
        };

        var entryType = await entryTypeAdminAppService.CreateAsync(input);

        entryType.ShouldNotBeNull();
        entryType.Name.ShouldBe(entryTypeName);
        entryType.FieldTabs.ShouldNotBeEmpty();
    }

    [Fact]
    public async Task CreateAsync_ShouldThrow_WithExistName()
    {
        var input = new CreateEntryTypeInput(testData.ChannelSectionId);
        input.DisplayName = "New Entry Type";
        input.Name = testData.ChannelSectionEntryTypeName;
        input.FieldTabs = new List<EntryFieldTabInput> {
            new EntryFieldTabInput{
                Name = "Default",
                Fields = new List<EntryFieldInput>
                {
                    new EntryFieldInput
                    {
                        FieldId=testData.TextboxFieldId,
                        ShowOnList = true,
                        DisplayName = "Textbox Field",
                        Required = true,
                    },
                    new EntryFieldInput
                    {
                        FieldId=testData.SelectFieldId,
                        ShowOnList = true,
                        DisplayName = "Select Field",
                        Required = true,
                    }
                }
            }
        };

        await Should.ThrowAsync<EntryTypeNameAlreadyExistException>(
            async () =>
                await entryTypeAdminAppService.CreateAsync(input)
            );
    }

    [Fact]
    public async Task UpdateAsync_ShouldWork()
    {
        var newDisplayName = "New Display Name";
        var entryType = await entryTypeAdminAppService.UpdateAsync(
            testData.ChannelSectionEntryTypeId, 
            new UpdateEntryTypeInput { 
                Name = testData.ChannelSectionEntryTypeName,
                DisplayName = newDisplayName,
                FieldTabs = new List<EntryFieldTabInput> {
                                new EntryFieldTabInput{
                                    Name = "Default",
                                    Fields = new List<EntryFieldInput>
                                    {
                                        new EntryFieldInput
                                        {
                                            FieldId=testData.TextboxFieldId,
                                            ShowOnList = true,
                                            DisplayName = "Textbox Field",
                                            Required = true,
                                        }
                                    }
                                }
                            }
            });

        var updatedEntryType = await entryTypeAdminAppService.GetAsync(testData.ChannelSectionEntryTypeId);

        updatedEntryType.DisplayName.ShouldBe(newDisplayName);
        updatedEntryType.FieldTabs[0].Fields.Count.ShouldBe(1);
    }

    [Fact]
    public async Task DeleteAsync_ShouldWork()
    {
        await entryTypeAdminAppService.DeleteAsync(testData.ChannelSectionEntryTypeId);

        await Should.ThrowAsync<EntityNotFoundException>(
            async () =>
                await entryTypeAdminAppService.GetAsync(testData.ChannelSectionEntryTypeId)
        );
    }
}
