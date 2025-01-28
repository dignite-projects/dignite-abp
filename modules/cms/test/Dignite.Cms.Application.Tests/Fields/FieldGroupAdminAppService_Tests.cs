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

namespace Dignite.Cms.FieldGroups;

public class FieldGroupAdminAppService_Tests : CmsApplicationTestBase
{
    private readonly IFieldGroupAdminAppService fieldGroupAdminAppService;
    private readonly CmsTestData testData;

    public FieldGroupAdminAppService_Tests()
    {
        fieldGroupAdminAppService = GetRequiredService<IFieldGroupAdminAppService>();
        testData = GetRequiredService<CmsTestData>();
    }


    [Fact]
    public async Task GetAsync()
    {
        var fieldGroup = await fieldGroupAdminAppService.GetAsync(testData.FieldGroupId);

        fieldGroup.Id.ShouldBe(testData.FieldGroupId);
    }

    [Fact]
    public async Task GetListAsync()
    {
        var fieldGroups = await fieldGroupAdminAppService.GetListAsync(new GetFieldGroupsInput());

        fieldGroups.TotalCount.ShouldBeGreaterThan(0);
        fieldGroups.Items.Any(x => x.Id == testData.FieldGroupId).ShouldBeTrue();
    }

    [Fact]
    public async Task CreateAsync_ShouldWork()
    {
        var name = "New Field Group";
        var fieldGroup = await fieldGroupAdminAppService.CreateAsync(
            new CreateOrUpdateFieldGroupInput()
            { 
                Name= name
            }
        );

        fieldGroup.ShouldNotBeNull();
        fieldGroup.Name.ShouldBe(name);
    }


    [Fact]
    public async Task UpdateAsync_ShouldWork()
    {
        var newName = "New Name";
        var fieldGroup = await fieldGroupAdminAppService.UpdateAsync(
            testData.FieldGroupId, 
            new CreateOrUpdateFieldGroupInput
            {
                Name = newName
            });

        var updatedFieldGroup = await fieldGroupAdminAppService.GetAsync(testData.FieldGroupId);

        updatedFieldGroup.Name.ShouldBe(newName);
    }

    [Fact]
    public async Task DeleteAsync_ShouldWork()
    {
        await fieldGroupAdminAppService.DeleteAsync(testData.FieldGroupId);

        await Should.ThrowAsync<EntityNotFoundException>(
            async () =>
                await fieldGroupAdminAppService.GetAsync(testData.FieldGroupId)
        );
    }
}
