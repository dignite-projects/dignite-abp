namespace Dignite.Abp.FieldCustomizing.Forms;

public interface IFormProvider
{
    /// <summary>
    /// Unique name of the field provider.
    /// </summary>
    string Name { get; }

    string DisplayName { get; }

    FormType FormType { get; }

    void Validate(FormValidateArgs args);

    FormConfigurationBase GetConfiguration(FormConfigurationDictionary fieldConfiguration);
}