using Dignite.Abp.DynamicForms;

namespace Dignite.Abp.DynamicForms.FileEdit;

public class FileEditFormProvider : FormProviderBase
{
    public const string ProviderName = "Upload";

    public override string Name => ProviderName;

    public override string DisplayName => L["UploadControl"];

    public override FormType FormType => FormType.Simple;

    public override void Validate(FormValidateArgs args)
    {
    }

    public override FormConfigurationBase GetConfiguration(FormConfigurationDictionary fieldConfiguration)
    {
        return new FileEditConfiguration(fieldConfiguration);
    }
}