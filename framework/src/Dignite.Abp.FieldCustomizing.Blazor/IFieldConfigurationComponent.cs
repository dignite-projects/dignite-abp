using System;

namespace Dignite.Abp.FieldCustomizing.Blazor
{
    public interface IFieldConfigurationComponent
    {
        Type FieldProviderType { get; }

        ICustomizeFieldDefinition Definition { get;  }
    }
}
