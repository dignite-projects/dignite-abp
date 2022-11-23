using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Blazorise.DataGrid;
using Blazorise.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Volo.Abp.AspNetCore.Components.Web.Extensibility.TableColumns;
using Volo.Abp.BlazoriseUI.Components;

namespace Dignite.Abp.BlazoriseUI.Components;

public partial class ExtensibleDataGrid<TItem> : ComponentBase
{
    protected const string DataFieldAttributeName = "Data";

    protected Dictionary<string, DataGridEntityActionsColumn<TItem>> ActionColumns =
        new Dictionary<string, DataGridEntityActionsColumn<TItem>>();

    protected Regex ExtensionPropertiesRegex = new Regex(@"ExtraProperties\[(.*?)\]");

    private string DataGridHeight { get; set; }

    private string ElementId { get; set; }
    public TItem SelectedItem { get; set; }
    public List<TItem> SelectedItems { get; set; }

    [Parameter] public IEnumerable<TItem> Data { get; set; }

    [Parameter] public EventCallback<DataGridReadDataEventArgs<TItem>> ReadData { get; set; }

    [Parameter] public int? TotalItems { get; set; }

    [Parameter] public bool ShowPager { get; set; }

    [Parameter] public int PageSize { get; set; }

    [Parameter] public int CurrentPage { get; set; } = 1;

    [Parameter] public IEnumerable<TableColumn> Columns { get; set; }

    [Parameter] public DataGridSelectionMode SelectionMode { get; set; }

    [Parameter] public int? ExtraHeight { get; set; }

    protected virtual RenderFragment RenderCustomTableColumnComponent(Type type, object data)
    {
        return (builder) =>
        {
            builder.OpenComponent(type);
            builder.AddAttribute(0, DataFieldAttributeName, data);
            builder.CloseComponent();
        };
    }

    protected virtual string GetConvertedFieldValue(TItem item, TableColumn columnDefinition)
    {
        var convertedValue = columnDefinition.ValueConverter.Invoke(item);
        if (!columnDefinition.DisplayFormat.IsNullOrEmpty())
        {
            return string.Format(columnDefinition.DisplayFormatProvider, columnDefinition.DisplayFormat,
                convertedValue);
        }

        return convertedValue;
    }

    protected override Task OnInitializedAsync()
    {
        ElementId = IdGenerator.Generate;
        return base.OnInitializedAsync();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender == true)
        {
            DataGridHeight = await JsRuntime.InvokeAsync<string>(
                "blazoriseUi.getDataGridHeight",
                ElementId,
                ExtraHeight.HasValue ? ExtraHeight.Value : "undefined"
                );
            StateHasChanged();
        }
    }
}