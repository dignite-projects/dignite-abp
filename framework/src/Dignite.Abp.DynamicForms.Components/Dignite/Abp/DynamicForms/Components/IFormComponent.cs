using System;

namespace Dignite.Abp.DynamicForms.Components;

public interface IFormComponent
{
    Type FormType { get; }
}