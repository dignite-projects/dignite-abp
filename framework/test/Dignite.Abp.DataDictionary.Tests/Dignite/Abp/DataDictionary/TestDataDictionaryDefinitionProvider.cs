using Dignite.Abp.DynamicForms.Select;

namespace Dignite.Abp.DataDictionary;

public class TestDataDictionaryDefinitionProvider : DataDictionaryDefinitionProvider
{
    public override void Define(IDataDictionaryDefinitionContext context)
    {
        var selectConfiguration = new SelectConfiguration()
        {
            NullText = "----",
            Options = new System.Collections.Generic.List<SelectListItem> {
                new SelectListItem("Item1","1",false),
                new SelectListItem("Item2","2",false)
            }
        };

        context.Add(
            new DataDictionaryDefinition(TestDataDictionaryNames.TestDataDictionaryWithoutDefaultValue, new DynamicForms.FormConfigurationDictionary()),
            new DataDictionaryDefinition(TestDataDictionaryNames.TestDataDictionaryWithDefaultValue, selectConfiguration.ConfigurationDictionary)
        );
    }
}
