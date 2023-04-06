using System.ComponentModel.DataAnnotations;

namespace Dignite.Abp.DynamicForms.CkEditor;

public class CkEditorConfiguration : FormConfigurationBase
{
    [Required]
    public string ImagesContainerName {
        get => ConfigurationDictionary.GetConfigurationOrDefault<string>(CkEditorConfigurationNames.ImagesContainerName, null);
        set => ConfigurationDictionary.SetConfiguration(CkEditorConfigurationNames.ImagesContainerName, value);
    }
    public string InitialContent {
        get => ConfigurationDictionary.GetConfigurationOrDefault<string>(CkEditorConfigurationNames.InitialContent, "");
        set => ConfigurationDictionary.SetConfiguration(CkEditorConfigurationNames.InitialContent, value);
    }


    public CkEditorConfiguration(FormConfigurationDictionary fieldConfiguration)
        : base(fieldConfiguration)
    {
    }

    public CkEditorConfiguration() : base()
    {
    }
}