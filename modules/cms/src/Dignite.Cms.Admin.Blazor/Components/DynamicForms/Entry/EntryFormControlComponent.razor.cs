using Blazorise;
using Blazorise.Components;
using Dignite.Cms.Admin.Entries;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Dignite.Cms.Admin.Blazor.Components.DynamicForms.Entry
{
    public partial class EntryFormControlComponent
    {
        [CascadingParameter]
        public CreateOrUpdateEntryInputBase EntryInput { get; set; }

        Guid? SelectionEntryId { get; set; }
        string SelectionText { get; set; }
        List<Guid> MultipleSelectionEntryIds = new();
        List<string> MultipleSelectionTexts = new();
        List<EntryDto> MultipleSelectionEntries=new();
        List<EntryDto> EntryDataSource = new List<EntryDto>();


        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            var value = Field.Value == null ?
                        new List<Guid>()
                        : JsonSerializer.Deserialize<List<Guid>>(Field.Value.ToString(), new JsonSerializerOptions(JsonSerializerDefaults.Web));
            if (value != null)
            {
                MultipleSelectionEntryIds = value;
                if (MultipleSelectionEntryIds.Any())
                {
                    MultipleSelectionEntries = (await EntryAdminAppService.GetListByIdsAsync(FormConfiguration.SectionId, MultipleSelectionEntryIds))
                                                .Items
                                                .ToList();

                    //When an entry in the data source has been deleted, the following code is used to reset the entry selected for this entry
                    MultipleSelectionEntryIds = MultipleSelectionEntries.Select(x => x.Id).ToList();
                }
                MergeSelectedData();

                //
                if (FormConfiguration.Multiple)
                {
                    MultipleSelectionTexts = EntryDataSource.Select(e => e.Title).ToList();
                }
                else
                {
                    if (MultipleSelectionEntries.Any())
                    {
                        SelectionEntryId = MultipleSelectionEntries[0].Id;
                        SelectionText = MultipleSelectionEntries[0].Title;
                        MultipleSelectionEntryIds.RemoveAll(x => x != SelectionEntryId);
                    }
                }

                await ChangeValueAsync(MultipleSelectionEntryIds);
            }
        }
        private async Task OnHandleReadData(AutocompleteReadDataEventArgs autocompleteReadDataEventArgs)
        {
            if (!EntryDataSource.Any(e => e.Title.Contains(autocompleteReadDataEventArgs.SearchValue, StringComparison.InvariantCultureIgnoreCase)))
            {
                if (!autocompleteReadDataEventArgs.CancellationToken.IsCancellationRequested)
                {
                    Random random = new();
                    await Task.Delay(random.Next(500));
                    if (!autocompleteReadDataEventArgs.CancellationToken.IsCancellationRequested)
                    {
                        EntryDataSource = (await EntryAdminAppService.GetListAsync(new GetEntriesInput
                        {
                            SectionId = FormConfiguration.SectionId,
                            Culture = EntryInput.Culture,
                            MaxResultCount = 100,
                            SkipCount = 0,
                            Status = Cms.Entries.EntryStatus.Published,
                            Filter = autocompleteReadDataEventArgs.SearchValue
                        })).Items.ToList();

                        //
                        MergeSelectedData();
                    }
                }
            }
        }

        async Task OnSelectedValueChanged(Guid? value)
        {
            if (!FormConfiguration.Multiple)
            {
                if (value.HasValue)
                {
                    SelectionEntryId = value.Value;
                    MultipleSelectionEntryIds.Clear();
                    MultipleSelectionEntryIds.Add(SelectionEntryId.Value);
                }
                else
                {
                    SelectionEntryId = null;
                    MultipleSelectionEntryIds.Clear();
                }
                await ChangeValueAsync(MultipleSelectionEntryIds);
            }
        }

        //
        async Task OnMultipleSelectedValuesChanged(List<Guid> values)
        {
            if (FormConfiguration.Multiple)
            {
                MultipleSelectionEntryIds = values;
                MultipleSelectionEntries = EntryDataSource.Where(e => values.Contains(e.Id)).ToList();
                await ChangeValueAsync(MultipleSelectionEntryIds);
            }
        }

        /*
         將已選中的數據合併到數據源中,Autocomplete控件的各種操作都跟該數據源相關,如果沒有正確設置數據源會出現莫名其妙的邏輯錯誤
         */
        void MergeSelectedData()
        {
            foreach (var entry in MultipleSelectionEntries)
            {
                if (!EntryDataSource.Any(e => e.Id == entry.Id))
                {
                    EntryDataSource.Add(entry);
                }
            }
        }

        void ValidateIsRequired(ValidatorEventArgs e)
        {
            if (Field.Required)
            {
                e.Status = MultipleSelectionEntryIds.Any() ? ValidationStatus.Success : ValidationStatus.Error;
            }
            else
            {
                e.Status = ValidationStatus.Success;
            }
        }
    }
}
