namespace Dignite.Abp.BlazoriseUI.Components;

public class DropNode<TNode>
{
    public DropNode(TNode node, TNode target, DragEnterNodePosition area)
    {
        Node = node;
        Target = target;
        Area = area;
    }

    public TNode Node { get; set; }
    public TNode Target { get; set; }
    public DragEnterNodePosition Area { get; set; }
}