using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Dignite.Abp.BlazoriseUI.Components;
public partial class TreeSelect<TNode, TValue> : ComponentBase
{
    protected bool ShowTree = false;

    protected string TextEditText;


    [Parameter] public IEnumerable<TNode> Nodes { get; set; }

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
    /// When clearing the selected nodes
    /// </summary>
    [Parameter] public EventCallback ClearNode { get; set; }

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
    [Parameter] public Func<TNode, string> NodeText { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Parameter] public Func<TNode, TValue> NodeValue { get; set; }

    protected override void OnParametersSet()
    {
        if (SelectedNode != null)
        {
            TextEditText = NodeText(SelectedNode);
        }

        base.OnParametersSet();
    }


    protected virtual async Task OnSelectChangedAsync(TNode args)
    {
        SelectedNode = args;
        TextEditText = NodeText(args);
        ShowTree = false;
        await SelectedNodeChanged.InvokeAsync(args);
    }

    protected void ShowTreeSelectToggle()
    {
        ShowTree = !ShowTree;
    }

    protected async Task ClearNodeAsync()
    {
        SelectedNode = default;
        TextEditText = null;
        ShowTree = false;
        await ClearNode.InvokeAsync();
    }
}
