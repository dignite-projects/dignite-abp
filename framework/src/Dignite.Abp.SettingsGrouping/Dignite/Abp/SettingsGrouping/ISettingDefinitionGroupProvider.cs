namespace Dignite.Abp.SettingsGrouping;

public interface ISettingDefinitionGroupProvider
{
    void Define(ISettingDefinitionGroupContext context);
}