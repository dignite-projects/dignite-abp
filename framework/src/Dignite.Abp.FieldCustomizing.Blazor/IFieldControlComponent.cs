using System;

namespace Dignite.Abp.FieldCustomizing.Blazor
{
    public interface IFieldControlComponent
    {
        Type FieldProviderType { get; }
    }
}
