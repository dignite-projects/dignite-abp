using System;

namespace Dignite.Abp.FieldCustomizing.Blazor
{
    public interface IFieldComponent
    {
        Type FieldProviderType { get; }
    }
}
