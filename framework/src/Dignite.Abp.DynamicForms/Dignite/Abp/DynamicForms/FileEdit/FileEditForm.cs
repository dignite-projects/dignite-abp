namespace Dignite.Abp.DynamicForms.FileEdit;

public class FileEditForm : FormBase
{
    public const string FileEditFormName = "FileEdit";

    public override string Name => FileEditFormName;

    public override string DisplayName => L["FileEditControl"];

    public override FormType FormType => FormType.Simple;

    public override void Validate(FormValidateArgs args)
    {
    }

    public override FormConfigurationBase GetConfiguration(FormConfigurationDictionary fieldConfiguration)
    {
        return new FileEditConfiguration(fieldConfiguration);
    }
}