namespace Dignite.Abp.DynamicForms;

public interface IFormControl
{
    /// <summary>
    /// Unique name of the form control
    /// </summary>
    string Name { get; }

    /// <summary>
    /// 
    /// </summary>
    string DisplayName { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="args"></param>
    void Validate(FormControlValidateArgs args);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="configuration"></param>
    /// <returns></returns>
    FormConfigurationBase GetConfiguration(FormConfigurationDictionary configuration);
}