namespace Dignite.Abp.FieldCustomizing.Fields.Upload;

public class UploadFieldProvider : FieldProviderBase
{
    public const string ProviderName = "Upload";

    public override string Name => ProviderName;

    public override string DisplayName => L["UploadControl"];

    public override FieldType ControlType => FieldType.Simple;

    public override void Validate(FieldValidateArgs args)
    {
    }

    public override FieldConfigurationBase GetConfiguration(FieldConfigurationDictionary fieldConfiguration)
    {
        return new UploadConfiguration(fieldConfiguration);
    }
}