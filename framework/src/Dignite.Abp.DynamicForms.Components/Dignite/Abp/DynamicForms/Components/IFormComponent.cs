using System;

namespace Dignite.Abp.DynamicForms.Components;

public interface IFormComponent
{
    Type FormProviderType { get; }
}