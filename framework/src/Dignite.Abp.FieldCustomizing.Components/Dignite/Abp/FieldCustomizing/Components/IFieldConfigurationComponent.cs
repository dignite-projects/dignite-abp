using System;

namespace Dignite.Abp.FieldCustomizing.Components;

public interface IFieldConfigurationComponent
{
    Type FieldProviderType { get; }

    ICustomizeFieldDefinition Definition { get; }
}