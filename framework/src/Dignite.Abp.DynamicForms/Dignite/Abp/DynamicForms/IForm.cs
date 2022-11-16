namespace Dignite.Abp.DynamicForms;

public interface IForm
{
    /// <summary>
    /// Unique name of the dynamic form
    /// </summary>
    string Name { get; }

    /// <summary>
    /// 
    /// </summary>
    string DisplayName { get; }

    /// <summary>
    /// 
    /// </summary>
    FormType FormType { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="args"></param>
    void Validate(FormValidateArgs args);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="configuration"></param>
    /// <returns></returns>
    FormConfigurationBase GetConfiguration(FormConfigurationDictionary configuration);
}