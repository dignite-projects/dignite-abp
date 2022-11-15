using System.ComponentModel.DataAnnotations;
using Dignite.Abp.DynamicForms;

namespace Dignite.Abp.DynamicForms.RichTextEditor;

public class RichTextEditorConfiguration : FormConfigurationBase
{
    [StringLength(256)]
    public string Placeholder {
        get => ConfigurationDictionary.GetConfigurationOrDefault<string>(RichTextEditorConfigurationNames.Placeholder, null);
        set => ConfigurationDictionary.SetConfiguration(RichTextEditorConfigurationNames.Placeholder, value);
    }

    [Required]
    public RichTextEditorMode Mode {
        get => ConfigurationDictionary.GetConfigurationOrDefault(RichTextEditorConfigurationNames.Mode, RichTextEditorMode.Classic);
        set => ConfigurationDictionary.SetConfiguration(RichTextEditorConfigurationNames.Mode, value);
    }

    public RichTextEditorConfiguration(FormConfigurationDictionary fieldConfiguration)
        : base(fieldConfiguration)
    {
    }

    public RichTextEditorConfiguration() : base()
    {
    }
}