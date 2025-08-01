using Blazorise;
using Dignite.Abp.Data;
using Dignite.Abp.DynamicForms;
using Dignite.Abp.Locales;
using Dignite.Cms.Admin.Entries;
using Dignite.Cms.Admin.Sections;
using Dignite.Cms.Localization;
using Dignite.Cms.Sections;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Dignite.Cms.Admin.Blazor.Pages.Cms.Admin.Entries
{

    public partial class CreateOrUpdateEntryComponent
    {
        [Parameter] public CreateOrUpdateEntryInputBase Entry { get; set; }
        [Parameter] public SectionDto Section { get; set; }

        /// <summary>
        /// This property is only valid when editing and is used to get the Id of the edited entry
        /// </summary>
        [CascadingParameter(Name = "EditingEntryId")]
        Guid? EditingEntryId { get; set; }

        protected EntryTypeDto CurrentEntryType { get; set; }
        public LocaleInfo Locale { get; private set; }
        protected IReadOnlyList<EntryDto> AllEntriesOfStructure;
        protected List<EntryDto> AllVersions = null;

        //Will not change again after assignment, used to verify that the slug already exists
        private string slugForValidation;
        private string cultureForValidation;

        public CreateOrUpdateEntryComponent()
        {
            LocalizationResource = typeof(CmsResource);
        }
        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            slugForValidation = Entry.Slug;
            cultureForValidation = Entry.Culture;
        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            CurrentEntryType = Section.EntryTypes.FirstOrDefault(et => et.Id == Entry.EntryTypeId);
            Locale = await LocaleProvider.GetLocaleAsync();
            await SetCultureAsync(Entry.Culture);
            await GetVersionsAsync();
        }

        protected async Task OnCultureSelectedValueChanged(string value)
        { 
            await SetCultureAsync(value);
        }

        protected async Task SetCultureAsync(string culture)
        { 
            Entry.Culture = culture;
            if (Section.Type == SectionType.Structure)
            {
                AllEntriesOfStructure =( await AppService.GetListAsync(new GetEntriesInput { 
                    SectionId=Section.Id,
                    Culture= culture,
                    MaxResultCount=1000
                })).Items;

                if (Entry.GetType() == typeof(UpdateEntryInput))
                {
                    AllEntriesOfStructure = AllEntriesOfStructure.Where(e => e.Slug != Entry.Slug).ToList();
                }
            }
        }
        protected async Task GetVersionsAsync()
        {
            if (Entry.GetType() == typeof(UpdateEntryInput))
            {
                AllVersions = (await AppService.GetAllVersionsAsync(EditingEntryId.Value))
                                .Items.ToList();
            }
        }

        private async Task ActivateAsync(Guid id)
        {
            await AppService.ActivateAsync(id);
            AllVersions = (await AppService.GetAllVersionsAsync(EditingEntryId.Value))
                            .Items.ToList();
            ((UpdateEntryInput)Entry).ConcurrencyStamp = AllVersions.Single(v => v.Id == id).ConcurrencyStamp;
        }
        private void EditVersion(Guid id)
        {
            Navigation.NavigateTo($"cms/admin/entries/{id}/edit",true);
        }
        private void NewVersion(Guid id)
        {
            Navigation.NavigateTo($"cms/admin/entries/create?RevisionEntryId={id}");
        }
        private async Task DeleteVersionAsync(Guid id) 
        { 
            AllVersions.RemoveAll(e => e.Id == id);
            await AppService.DeleteAsync(id);
        }

        private void OnFieldValueChanged(FormField field)
        { 
            Entry.SetField(field.Name, field.Value);
        }

        private async Task SlugExistsValidatorAsync(ValidatorEventArgs e, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var slug = Convert.ToString(e.Value);
            if (!slug.IsNullOrEmpty())
            {
                if (!slug.Equals(slugForValidation, StringComparison.InvariantCultureIgnoreCase))
                {
                    await ValidateSlugExistsAsync(e);
                }
            }
            else
            {
                e.Status = ValidationStatus.Error;
            }
        }

        private async Task CultureExistsValidatorAsync(ValidatorEventArgs e, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var culture = Convert.ToString(e.Value);

            if (Section.Type == SectionType.Single)
            {
                if (Entry.GetType() == typeof(CreateEntryInput))
                {
                    if (!((CreateEntryInput)Entry).InitialVersionId.HasValue)
                        await ValidateEntryTypeExistsAsync(e);
                }
                else if (Entry.GetType() == typeof(UpdateEntryInput))
                {
                    if (!culture.Equals(cultureForValidation, StringComparison.InvariantCultureIgnoreCase))
                    {
                        await ValidateEntryTypeExistsAsync(e);
                    }
                }
            }

            if (e.Status == ValidationStatus.Success)
            {
                if (!culture.Equals(cultureForValidation, StringComparison.InvariantCultureIgnoreCase))
                {
                    await ValidateSlugExistsAsync(e);
                }
            }
        }

        private async Task ValidateSlugExistsAsync(ValidatorEventArgs e)
        {
            e.Status = await AppService.SlugExistsAsync(new SlugExistsInput(Entry.Culture,Section.Id,Entry.Slug))
                ? ValidationStatus.Error
                : ValidationStatus.Success;

            e.ErrorText = L["EntrySlug{0}AlreadyExist", Entry.Slug];
        }

        private async Task ValidateEntryTypeExistsAsync(ValidatorEventArgs e)
        {
            e.Status = await AppService.CultureExistWithSingleSectionAsync(new CultureExistWithSingleSectionInput(Entry.Culture, Section.Id, Entry.EntryTypeId))
                ? ValidationStatus.Error
                : ValidationStatus.Success;

            e.ErrorText = L["EntriesAlreadyExistEntryType", CurrentEntryType.DisplayName, Locale.AvailableCultures.FirstOrDefault(l=>l.Name==Entry.Culture)?.DisplayName];
        }
    }
}
