namespace Dignite.Abp.BlazoriseUI.Components;

public class DropNode<TNode>
{
    public DropNode(TNode source, TNode target, DragEnterNodePosition position)
    {
        Source = source;
        Target = target;
        Position = position;
    }

    public TNode Source { get; set; }
    public TNode Target { get; set; }
    public DragEnterNodePosition Position { get; set; }
}