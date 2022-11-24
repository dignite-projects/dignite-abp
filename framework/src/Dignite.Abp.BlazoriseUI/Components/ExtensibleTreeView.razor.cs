using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace Dignite.Abp.BlazoriseUI.Components;
public partial class ExtensibleTreeView<TNode>: ComponentBase
{
    #region Members

    /// <summary>
    /// 
    /// </summary>
    protected TNode DraggingNode;

    /// <summary>
    /// 
    /// </summary>
    private TNode DragEnterNode;

    /// <summary>
    /// 
    /// </summary>
    private DragEnterNodePosition? DragEnterArea;
    #endregion

    #region Properties
    [Parameter] public IEnumerable<TNode> Nodes { get; set; }


    /// <summary>
    /// Template to display content for the node
    /// </summary>
    [Parameter] public RenderFragment<TNode> NodeContent { get; set; }

    /// <summary>
    /// Currently selected TreeView item/node.
    /// </summary>
    [Parameter]
    public TNode SelectedNode { get; set; }

    /// <summary>
    /// Occurs when the selected TreeView node has changed.
    /// </summary>
    [Parameter] public EventCallback<TNode> SelectedNodeChanged { get; set; }

    /// <summary>
    /// List of currently expanded TreeView items (child nodes).
    /// </summary>
    [Parameter] public IList<TNode> ExpandedNodes { get; set; } = new List<TNode>();

    /// <summary>
    /// Occurs when the collection of expanded nodes has changed.
    /// </summary>
    [Parameter] public EventCallback<IList<TNode>> ExpandedNodesChanged { get; set; }

    /// <summary>
    /// Gets the list of child nodes for each node.
    /// </summary>
    [Parameter] public Func<TNode, IEnumerable<TNode>> GetChildNodes { get; set; }

    /// <summary>
    /// Indicates if the node has child elements.
    /// </summary>
    [Parameter] public Func<TNode, bool> HasChildNodes { get; set; } = node => true;

    /// <summary>
    /// 
    /// </summary>
    [Parameter] public bool Draggable { get; set; }

    [Parameter] public EventCallback<DropNode<TNode>> NodeDropped { get; set; }
    #endregion

    #region Methods
    void DragEnter(TNode node, DragEnterNodePosition area)
    {
        DragEnterNode = node;
        DragEnterArea = area;
    }

    async void Dropped()
    {
        await NodeDropped.InvokeAsync(new DropNode<TNode>(DraggingNode, DragEnterNode, DragEnterArea.Value));

        await this.InvokeAsync(() => this.StateHasChanged());
    }
    #endregion

}
