

namespace Dignite.Abp.AspNetCore.Components.CkEditor;

public record DynamicCkEditorPlugin<T> : ICkEditorPlugin<T>
{
    public string Name { get; init; }
    public T Options { get; init; }
}
