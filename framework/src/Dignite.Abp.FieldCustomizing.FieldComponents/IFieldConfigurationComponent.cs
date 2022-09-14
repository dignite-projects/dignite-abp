using System;

namespace Dignite.Abp.FieldCustomizing.FieldComponents;

public interface IFieldConfigurationComponent
{
    Type FieldProviderType { get; }

    ICustomizeFieldDefinition Definition { get; }
}