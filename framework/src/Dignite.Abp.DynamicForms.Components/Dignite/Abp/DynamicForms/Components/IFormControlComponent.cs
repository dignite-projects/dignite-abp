using System;
using Microsoft.AspNetCore.Components;

namespace Dignite.Abp.DynamicForms.Components;

public interface IFormControlComponent
{
    Type FormControlType { get; }
    public FormField Field { get; }
    public EventCallback<FormField> OnChangedValueAsync { get; set; }
}