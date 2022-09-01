using System;
using Blazorise.Extensions;
using Blazorise.DataGrid;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Localization;
using Volo.Abp.AspNetCore.Components.Web.Extensibility.TableColumns;
using Volo.Abp.BlazoriseUI.Components;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Blazorise;

namespace Dignite.Abp.BlazoriseUI.Components
{
    public partial class DigniteAbpExtensibleDataGrid<TItem> : ComponentBase
    {
        protected const string DataFieldAttributeName = "Data";

        protected Dictionary<string, DataGridEntityActionsColumn<TItem>> ActionColumns =
            new Dictionary<string, DataGridEntityActionsColumn<TItem>>();

        protected Regex ExtensionPropertiesRegex = new Regex(@"ExtraProperties\[(.*?)\]");

        string DataGridHeight { get; set; }

        string ElementId { get; set; }
        public TItem SelectedItem { get; set; }
        public List<TItem> SelectedItems { get; set; }

        [Parameter] public IEnumerable<TItem> Data { get; set; }

        [Parameter] public EventCallback<DataGridReadDataEventArgs<TItem>> ReadData { get; set; }

        [Parameter] public int? TotalItems { get; set; }

        [Parameter] public bool ShowPager { get; set; }

        [Parameter] public int PageSize { get; set; }

        [Parameter] public IEnumerable<TableColumn> Columns { get; set; }
        [Parameter] public DataGridSelectionMode SelectionMode { get; set; }

        [Parameter] public int CurrentPage { get; set; } = 1;

        [Inject]
        public IStringLocalizerFactory StringLocalizerFactory { get; set; }

        [Inject]
        public IJSRuntime JsRuntime { get; set; }

        [Inject] public IIdGenerator IdGenerator { get; set; }


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
                DataGridHeight = await JsRuntime.InvokeAsync<string>("blazoriseUi.getDataGridHeight", ElementId);
                StateHasChanged();
            }
        }

    }
}