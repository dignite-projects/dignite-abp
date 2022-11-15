using Volo.Abp.DependencyInjection;
using Volo.Abp.Localization;
using Volo.Abp.Settings;

namespace Dignite.Abp.SettingsGrouping;

public abstract class SettingDefinitionGroupProvider : SettingDefinitionProvider, ISettingDefinitionGroupProvider, ITransientDependency
{
    public virtual string Section { get; protected set; }
}