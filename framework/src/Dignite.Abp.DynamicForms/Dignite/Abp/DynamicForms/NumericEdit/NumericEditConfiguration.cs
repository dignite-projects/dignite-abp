﻿using Dignite.Abp.DynamicForms;

namespace Dignite.Abp.DynamicForms.NumericEdit;

public class NumericEditConfiguration : FormConfigurationBase
{
    /// <summary>
    /// Maximum number of decimal places after the decimal separator.
    /// </summary>
    public int Decimals {
        get => ConfigurationDictionary.GetConfiguration<int>(NumericEditConfigurationNames.Decimals, 2);
        set => ConfigurationDictionary.SetConfiguration(NumericEditConfigurationNames.Decimals, value);
    }

    public decimal? Max {
        get => ConfigurationDictionary.GetConfiguration<decimal?>(NumericEditConfigurationNames.Max);
        set => ConfigurationDictionary.SetConfiguration(NumericEditConfigurationNames.Max, value);
    }

    public decimal? Min {
        get => ConfigurationDictionary.GetConfiguration<decimal?>(NumericEditConfigurationNames.Min);
        set => ConfigurationDictionary.SetConfiguration(NumericEditConfigurationNames.Min, value);
    }

    /// <summary>
    /// Specifies the interval between valid values.
    /// </summary>
    public decimal? Step {
        get => ConfigurationDictionary.GetConfiguration<decimal?>(NumericEditConfigurationNames.Step);
        set => ConfigurationDictionary.SetConfiguration(NumericEditConfigurationNames.Step, value);
    }

    /// <summary>
    /// Format Specifier
    /// </summary>
    public string FormatSpecifier {
        get => ConfigurationDictionary.GetConfiguration<string>(NumericEditConfigurationNames.FormatSpecifier, null);
        set => ConfigurationDictionary.SetConfiguration(NumericEditConfigurationNames.FormatSpecifier, value);
    }

    public NumericEditConfiguration(FormConfigurationDictionary fieldConfiguration)
        : base(fieldConfiguration)
    {
    }

    public NumericEditConfiguration() : base()
    {
    }
}