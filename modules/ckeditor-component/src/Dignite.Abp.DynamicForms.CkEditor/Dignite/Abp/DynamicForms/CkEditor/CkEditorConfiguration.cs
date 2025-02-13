namespace Dignite.Abp.DynamicForms.CkEditor;

public class CkEditorConfiguration : FormConfigurationBase
{
    public CkEditorMode Mode {
        get => ConfigurationDictionary.GetConfiguration<CkEditorMode>(CkEditorConfigurationNames.Mode, CkEditorMode.Classic);
        set => ConfigurationDictionary.SetConfiguration(CkEditorConfigurationNames.Mode, value);
    }

    public string ImagesContainerName {
        get => ConfigurationDictionary.GetConfiguration<string>(CkEditorConfigurationNames.ImagesContainerName, null);
        set => ConfigurationDictionary.SetConfiguration(CkEditorConfigurationNames.ImagesContainerName, value);
    }
    public string InitialContent {
        get => ConfigurationDictionary.GetConfiguration<string>(CkEditorConfigurationNames.InitialContent, "");
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