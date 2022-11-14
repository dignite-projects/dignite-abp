using System;

namespace Dignite.Abp.FieldCustomizing.Components;

public interface IFieldFormComponent
{
    Type FormProviderType { get; }
}