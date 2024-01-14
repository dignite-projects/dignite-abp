using System;

namespace Dignite.Abp.DynamicForms.Components;

public interface IFormViewComponent
{
    Type FormControlType { get; }
    public FormField Field { get; }
}