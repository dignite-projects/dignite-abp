namespace Dignite.Abp.FieldCustomizing.Fields.NumericEdit;

public class NumericEditConfiguration : FieldConfigurationBase
{
    /// <summary>
    /// Maximum number of decimal places after the decimal separator.
    /// </summary>
    public int Decimals {
        get => ConfigurationDictionary.GetConfigurationOrDefault<int>(NumericEditConfigurationNames.Decimals, 2);
        set => ConfigurationDictionary.SetConfiguration(NumericEditConfigurationNames.Decimals, value);
    }


    public decimal? Max {
        get => ConfigurationDictionary.GetConfigurationOrDefault<decimal?>(NumericEditConfigurationNames.Max);
        set => ConfigurationDictionary.SetConfiguration(NumericEditConfigurationNames.Max, value);
    }

    public decimal? Min {
        get => ConfigurationDictionary.GetConfigurationOrDefault<decimal?>(NumericEditConfigurationNames.Min);
        set => ConfigurationDictionary.SetConfiguration(NumericEditConfigurationNames.Min, value);
    }

    /// <summary>
    /// Specifies the interval between valid values.
    /// </summary>
    public decimal? Step {
        get => ConfigurationDictionary.GetConfigurationOrDefault<decimal?>(NumericEditConfigurationNames.Step);
        set => ConfigurationDictionary.SetConfiguration(NumericEditConfigurationNames.Step, value);
    }

    /// <summary>
    /// Format Specifier
    /// </summary>
    public string FormatSpecifier {
        get => ConfigurationDictionary.GetConfigurationOrDefault<string>(NumericEditConfigurationNames.FormatSpecifier, null);
        set => ConfigurationDictionary.SetConfiguration(NumericEditConfigurationNames.FormatSpecifier, value);
    }

    public NumericEditConfiguration(FieldConfigurationDictionary fieldConfiguration)
        : base(fieldConfiguration)
    {
    }

    public NumericEditConfiguration() : base()
    {
    }
}
