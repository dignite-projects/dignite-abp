using System;

namespace Dignite.Abp.DynamicForms.Components;

public interface IFieldComponent
{
    Type FormType { get; }
}