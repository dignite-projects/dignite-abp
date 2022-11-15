using System;

namespace Dignite.Abp.DynamicForms.Components;

public interface IFieldComponent
{
    Type FormProviderType { get; }
}