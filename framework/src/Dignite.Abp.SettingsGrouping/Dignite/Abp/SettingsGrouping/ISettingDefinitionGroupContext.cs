using JetBrains.Annotations;
using Volo.Abp.Settings;

namespace Dignite.Abp.SettingsGrouping;

public interface ISettingDefinitionGroupContext : ISettingDefinitionContext
{
    /// <summary>
    /// Group of settings
    /// </summary>
    public SettingDefinitionGroup Group { get; set; }

    /// <summary>
    ///
    /// </summary>
    /// <param name="group"></param>
    /// <param name="definitions"></param>
    void Add([NotNull] SettingDefinitionGroup group, [NotNull] params SettingDefinitionSection[] definitions);
}