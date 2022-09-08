using System;

namespace Dignite.Abp.FieldCustomizing.FieldComponents;

public interface IFieldComponent
{
    Type FieldProviderType { get; }
}
