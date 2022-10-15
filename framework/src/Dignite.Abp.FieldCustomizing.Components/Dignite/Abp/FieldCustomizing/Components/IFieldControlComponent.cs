using System;

namespace Dignite.Abp.FieldCustomizing.Components;

public interface IFieldControlComponent
{
    Type FieldProviderType { get; }
}