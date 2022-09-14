using System;

namespace Dignite.Abp.FieldCustomizing.FieldComponents;

public interface IFieldControlComponent
{
    Type FieldProviderType { get; }
}