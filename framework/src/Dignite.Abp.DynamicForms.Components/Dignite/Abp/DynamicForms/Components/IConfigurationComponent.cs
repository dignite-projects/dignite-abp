using System;

namespace Dignite.Abp.DynamicForms.Components;

public interface IConfigurationComponent
{
    Type FormType { get; }
}