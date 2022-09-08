using System.ComponentModel.DataAnnotations;

namespace Dignite.Abp.FieldCustomizing.Fields.RichTextEditor;

public class RichTextEditorConfiguration : FieldConfigurationBase
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


    public RichTextEditorConfiguration(FieldConfigurationDictionary fieldConfiguration)
        : base(fieldConfiguration)
    {
    }

    public RichTextEditorConfiguration() : base()
    {
    }
}
