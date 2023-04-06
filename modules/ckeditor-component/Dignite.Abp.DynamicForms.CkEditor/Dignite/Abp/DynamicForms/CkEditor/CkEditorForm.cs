namespace Dignite.Abp.DynamicForms.CkEditor;

public class CkEditorForm : FormBase
{
    public const string CkEditorFormName = "CkEditor";

    public override string Name => CkEditorFormName;

    public override string DisplayName => L["CkEditorFormDisplayName"];

    public override FormType FormType => FormType.Simple;

    public override void Validate(FormValidateArgs args)
    {
    }

    public override FormConfigurationBase GetConfiguration(FormConfigurationDictionary fieldConfiguration)
    {
        return new CkEditorConfiguration(fieldConfiguration);
    }
}