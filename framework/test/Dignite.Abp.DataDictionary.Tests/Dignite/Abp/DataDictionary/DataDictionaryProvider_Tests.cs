using System.Collections.Generic;
using System.Threading.Tasks;
using Dignite.Abp.DynamicForms.Select;
using Shouldly;
using Volo.Abp;
using Volo.Abp.Testing;
using Xunit;
using Dignite.Abp.DynamicForms;

namespace Dignite.Abp.DataDictionary;

public class DataDictionaryProvider_Tests : AbpIntegratedTest<DigniteAbpDataDictionaryTestModule>
{
    private readonly IDataDictionaryProvider _settingProvider;

    public DataDictionaryProvider_Tests()
    {
        _settingProvider = GetRequiredService<IDataDictionaryProvider>();
    }

    protected override void SetAbpApplicationCreationOptions(AbpApplicationCreationOptions options)
    {
        options.UseAutofac();
    }

    [Fact]
    public async Task Should_Get_Null_If_No_Value_Provided_And_No_Default_Value()
    {
        (await _settingProvider.GetOrNullAsync(TestDataDictionaryNames.TestDataDictionaryWithoutDefaultValue))
            .Count.ShouldBe(0);
    }

    [Fact]
    public async Task Should_Get_Default_Value_If_No_Value_Provided_And_There_Is_A_Default_Value()
    {
        var configuration = await _settingProvider.GetOrNullAsync(TestDataDictionaryNames.TestDataDictionaryWithDefaultValue);
        configuration.GetConfigurationOrDefault(SelectConfigurationNames.Options, new List<SelectListItem>())
            .Count
            .ShouldBe(2);
    }
}
