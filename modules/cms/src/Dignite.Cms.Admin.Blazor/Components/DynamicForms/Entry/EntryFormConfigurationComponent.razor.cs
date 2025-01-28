using Blazorise;
using Dignite.Cms.Admin.Sections;
using Dignite.Cms.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dignite.Cms.Admin.Blazor.Components.DynamicForms.Entry
{
    public partial class EntryFormConfigurationComponent
    {
        private readonly ISectionAdminAppService _sectionAdminAppService;

        protected IReadOnlyList<SectionDto> Sections { get; set; } = new List<SectionDto>();


        public EntryFormConfigurationComponent(ISectionAdminAppService sectionAdminAppService)
        {
            LocalizationResource = typeof(CmsResource);
            _sectionAdminAppService = sectionAdminAppService;
        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            if (FormConfiguration.SectionId!= Guid.Empty)
            {
                Sections = (await _sectionAdminAppService.GetListAsync(
                    new GetSectionsInput()
                    {
                        MaxResultCount = 1000
                    })).Items;
            }
        }

        private void SectionSelectedValidator(ValidatorEventArgs e)
        {
            if (FormConfiguration.SectionId != Guid.Empty)
            {
                e.Status = ValidationStatus.Success;
            }
            else
            {
                e.Status = ValidationStatus.Error;
            }
        }
    }
}
