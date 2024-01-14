using System;

namespace Dignite.Abp.DynamicForms.Components;

public interface IFormConfigurationComponent
{
    Type FormControlType { get; }
    public FormConfigurationDictionary ConfigurationDictionary { get; }
}