using Dignite.Abp.DynamicForms.DateEdit;
using Dignite.Abp.DynamicForms.NumericEdit;
using Shouldly;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Dignite.Cms.Fields;

public class FieldManager_Tests : CmsDomainTestBase
{
    private readonly CmsTestData testData;
    private readonly FieldManager fieldManager;
    private readonly IFieldRepository fieldRepository;

    public FieldManager_Tests()
    {
        fieldManager = GetRequiredService<FieldManager>();
        testData = GetRequiredService<CmsTestData>();
        fieldRepository = GetRequiredService<IFieldRepository>();
    }



    [Fact]
    public async Task CreateAsync_ShouldWorkProperly()
    {
        var configuration = new NumericEditConfiguration();
        configuration.Decimals = 2;
        configuration.Max = 10000;
        configuration.Min = 1;
        configuration.FormatSpecifier = "C";

        var field = await fieldManager.CreateAsync(
            testData.FieldGroupId,
            "NumericEdit-Field-Name",
            "NumericEdit Field",
            "",
            NumericEditFormControl.ControlName,
            configuration.ConfigurationDictionary,
            null
        );
        field.Id.ShouldNotBe(Guid.Empty);

        var fieldFromDb = await fieldRepository.GetAsync(field.Id);

        fieldFromDb.Name.ShouldBe(field.Name);
        fieldFromDb.FormControlName.ShouldBe(field.FormControlName);
        fieldFromDb.DisplayName.ShouldBe(field.DisplayName);
        fieldFromDb.GroupId.ShouldBe(field.GroupId);

        var configurationFromDb = new NumericEditConfiguration(fieldFromDb.FormConfiguration);
        configurationFromDb.Decimals.ShouldBe(configuration.Decimals);
        configurationFromDb.Max.ShouldBe(configuration.Max);
        configurationFromDb.Min.ShouldBe(configuration.Min);
        configurationFromDb.FormatSpecifier.ShouldBe(configuration.FormatSpecifier);

    }


    [Fact]
    public async Task CreateAsync_ShouldThrowException_WithNonExistingName()
    {
        var configuration = new DateEditConfiguration();
        configuration.InputMode = DateInputMode.Date;

        var exception = await Should.ThrowAsync<FieldNameAlreadyExistException>(
            async () => await fieldManager.CreateAsync(
                testData.FieldGroupId,
                testData.TextboxFieldName,
                "DateEdit Field",
                "",
                DateEditFormControl.ControlName,
                configuration.ConfigurationDictionary,
                null
            ));

        exception.ShouldNotBeNull();
        exception.Code.ShouldBe(CmsErrorCodes.Fields.NameAlreadyExist);
    }
}
