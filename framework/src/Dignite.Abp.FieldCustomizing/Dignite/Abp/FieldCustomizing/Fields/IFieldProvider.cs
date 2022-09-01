namespace Dignite.Abp.FieldCustomizing.Fields
{
    public interface IFieldProvider
    {
        /// <summary>
        /// Unique name of the field provider.
        /// </summary>
        string Name { get; }

        string DisplayName { get; }

        FieldType ControlType { get; }

        void Validate(FieldValidateArgs args);

        FieldConfigurationBase GetConfiguration(FieldConfigurationDictionary fieldConfiguration);
    }
}
