using System;

namespace Dignite.Abp.FieldCustomizing.Components;

public interface IFieldConfigurationComponent
{
    Type FormProviderType { get; }

    ICustomizeFieldDefinition FieldDefinition { get; }
}