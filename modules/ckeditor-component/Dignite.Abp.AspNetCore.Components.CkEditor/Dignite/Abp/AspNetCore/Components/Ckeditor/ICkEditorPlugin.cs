
namespace Dignite.Abp.AspNetCore.Components.CkEditor;

public interface ICkEditorPlugin<T>
{
    public string Name { get; }
    public T Options { get; }
}
