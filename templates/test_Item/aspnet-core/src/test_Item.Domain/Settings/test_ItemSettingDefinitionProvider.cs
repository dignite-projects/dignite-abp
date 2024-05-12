using Volo.Abp.Settings;

namespace test_Item.Settings;

public class test_ItemSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(test_ItemSettings.MySetting1));
    }
}
