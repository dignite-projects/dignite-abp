using Dignite.Abp.DynamicForms.NumericEdit;
using Dignite.Abp.DynamicForms.Select;
using Dignite.Abp.Files;
using Dignite.Cms.Admin.Fields;
using Shouldly;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Xunit;

namespace Dignite.Cms.Fields;

public class FieldAdminAppService_Tests : CmsApplicationTestBase
{
    private readonly IFieldAdminAppService fieldAdminAppService;
    private readonly CmsTestData testData;

    public FieldAdminAppService_Tests()
    {
        fieldAdminAppService = GetRequiredService<IFieldAdminAppService>();
        testData = GetRequiredService<CmsTestData>();
    }


    [Fact]
    public async Task GetAsync()
    {
        var field = await fieldAdminAppService.GetAsync(testData.SelectFieldId);

        field.Name.ShouldBe(testData.SelectFieldName);
        (new SelectConfiguration(field.FormConfiguration)).Options.ShouldNotBeEmpty();
    }

    [Fact]
    public async Task GetListAsync()
    {
        var fields = await fieldAdminAppService.GetListAsync(new GetFieldsInput());

        fields.TotalCount.ShouldBeGreaterThan(0);
        fields.Items.Any(x => x.Name == testData.SelectFieldName).ShouldBeTrue();
    }

    [Fact]
    public async Task NameExistsAsync()
    {
        var result = await fieldAdminAppService.NameExistsAsync(testData.SelectFieldName);

        result.ShouldBe(true);
    }

    [Fact]
    public async Task CreateAsync_ShouldWork()
    {
        var fieldName = "new-numeric-field-name";
        var configuration = new NumericEditConfiguration();
        configuration.Decimals = 2;
        configuration.Max = 10000;
        configuration.Min = 1;
        configuration.FormatSpecifier = "C";

        var field = await fieldAdminAppService.CreateAsync(
            new CreateFieldInput { 
                DisplayName= "NumericEdit Field",
                FormConfiguration=configuration.ConfigurationDictionary,
                FormControlName=NumericEditFormControl.ControlName,
                Name= fieldName
            }
        );

        field.ShouldNotBeNull();
        field.Name.ShouldBe(fieldName);
        (new NumericEditConfiguration(field.FormConfiguration)).FormatSpecifier.ShouldBe("C");
    }

    [Fact]
    public async Task CreateAsync_ShouldThrow_WithExistName()
    {
        var configuration = new NumericEditConfiguration();
        configuration.Decimals = 2;
        configuration.Max = 10000;
        configuration.Min = 1;
        configuration.FormatSpecifier = "C";

        await Should.ThrowAsync<FieldNameAlreadyExistException>(
            async () =>
                await fieldAdminAppService.CreateAsync(
                    new CreateFieldInput
                    {
                        DisplayName = "NumericEdit Field",
                        FormConfiguration = configuration.ConfigurationDictionary,
                        FormControlName = NumericEditFormControl.ControlName,
                        Name = testData.SelectFieldName
                    }
                )
            );
    }


    [Fact]
    public async Task UpdateAsync_ShouldWork()
    {
        var selectFormConfiguration = new SelectConfiguration();
        selectFormConfiguration.Multiple = false;
        selectFormConfiguration.Options = new List<SelectListItem> {
            new SelectListItem("Item 1",testData.SelectFieldItem1Value,true),
            new SelectListItem("Item 2",testData.SelectFieldItem2Value,false),
            new SelectListItem("Item 2",testData.SelectFieldItem3Value,false)
        };

        var newDisplayName = "New Display Name";
        var field = await fieldAdminAppService.UpdateAsync(
            testData.SelectFieldId, 
            new UpdateFieldInput
            {
                GroupId = testData.FieldGroupId,
                DisplayName = newDisplayName,
                Name = testData.SelectFieldName,
                FormControlName = SelectFormControl.ControlName,
                FormConfiguration = selectFormConfiguration.ConfigurationDictionary
            });

        var updatedField = await fieldAdminAppService.GetAsync(testData.SelectFieldId);

        updatedField.DisplayName.ShouldBe(newDisplayName);
    }

    [Fact]
    public async Task DeleteAsync_ShouldWork()
    {
        await fieldAdminAppService.DeleteAsync(testData.SelectFieldId);

        await Should.ThrowAsync<EntityNotFoundException>(
            async () =>
                await fieldAdminAppService.GetAsync(testData.SelectFieldId)
        );
    }
}
