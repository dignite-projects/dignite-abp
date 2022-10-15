using System;

namespace Dignite.Abp.FieldCustomizing.Components;

public interface IFieldComponent
{
    Type FieldProviderType { get; }
}