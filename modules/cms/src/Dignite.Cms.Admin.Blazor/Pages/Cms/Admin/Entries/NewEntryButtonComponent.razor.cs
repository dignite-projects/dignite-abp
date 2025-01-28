using Dignite.Cms.Admin.Sections;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using System;

namespace Dignite.Cms.Admin.Blazor.Pages.Cms.Admin.Entries
{
    public partial class NewEntryButtonComponent: ComponentBase
    {
        [Parameter]
        public LocalizedString ButtonText { get; set; }

        [Parameter]
        public string CultureName { get; set; }


        [Parameter]
        public SectionDto Section{ get; set; }

        private void OnEntryTypeClick(Guid entryTypeId)
        {
            Navigation.NavigateTo($"cms/admin/entries/create?cultureName={CultureName}&sectionId={Section.Id}&entryTypeId={entryTypeId}");
        }
    }
}
