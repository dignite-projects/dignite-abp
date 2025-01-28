using Blazorise;
using Dignite.Cms.Admin.Sections;
using Dignite.Cms.Localization;
using Dignite.Cms.Sections;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Dignite.Cms.Admin.Blazor.Pages.Cms.Admin.Sections
{
    public partial class CreateOrUpdateSectionComponent
    {
        [Parameter] public CreateOrUpdateSectionInputBase Entity { get; set; }

        protected IReadOnlyList<SectionType> SectionTypes { get; set; }

        //Will not change again after assignment, used to verify that the site name already exists
        private string sectionNameForValidation;
        private string sectionRouteForValidation;

        public CreateOrUpdateSectionComponent()
        {
            LocalizationResource = typeof(CmsResource);
            SectionTypes = Enum.GetValues(typeof(SectionType))
                                .Cast<SectionType>()
                                .ToList();
        }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            sectionNameForValidation = Entity.Name;
            sectionRouteForValidation = Entity.Route;
        }

        private async Task NameValidatorAsync(ValidatorEventArgs e, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var name = Convert.ToString(e.Value);
            if (!name.IsNullOrEmpty())
            {
                if (!name.Equals(sectionNameForValidation, StringComparison.InvariantCultureIgnoreCase))
                {
                    e.Status = await _sectionAdminAppService.NameExistsAsync(new SectionNameExistsInput(name))
                        ? ValidationStatus.Error
                        : ValidationStatus.Success;

                    e.ErrorText = L["SectionName{0}AlreadyExist", name];
                }
            }
            else
            {
                e.Status = ValidationStatus.Error;
            }
        }

        private void SectionTypeValidatorAsync(ValidatorEventArgs e)
        {
            var sectionType = (SectionType)e.Value;
            if (sectionType != SectionType.Single)
            {
                Entity.IsDefault = false;
            }
        }

        private async Task RouteValidatorAsync(ValidatorEventArgs e, CancellationToken cancellationToken)
        {
            var route = Convert.ToString(e.Value);
            if (Entity.Type != SectionType.Single && !route.IsNullOrWhiteSpace())
            {
                e.Status = !route.Contains("{" + nameof(Dignite.Cms.Admin.Entries.EntryDto.Slug) + "}", StringComparison.InvariantCultureIgnoreCase)
                    ? ValidationStatus.Error
                    : ValidationStatus.Success;

                e.ErrorText = L["RouteVerificationTips", L[Entity.Type.ToLocalizationKey()], "{" + nameof(Dignite.Cms.Admin.Entries.EntryDto.Slug) + "}"];
            }

            if (e.Status != ValidationStatus.Error)
            {
                if (!route.IsNullOrWhiteSpace())
                {
                    if (!route.Equals(sectionRouteForValidation, StringComparison.InvariantCultureIgnoreCase))
                    {
                        e.Status = await _sectionAdminAppService.RouteExistsAsync(new SectionRouteExistsInput(route))
                            ? ValidationStatus.Error
                            : ValidationStatus.Success;

                        e.ErrorText = L["SectionRoute{0}AlreadyExist", route];
                    }
                }
            }
        }

        void DisplayNameTextEditBlur()
        {
            if (!Entity.DisplayName.IsNullOrEmpty() && Entity.Name.IsNullOrEmpty())
            {
                Entity.Name = SlugNormalizer.Normalize(Entity.DisplayName);

                //
                if (Entity.Route.IsNullOrEmpty())
                {
                    if (Entity.Type == SectionType.Single)
                    {
                        Entity.Route = Entity.Name;
                    }
                    else
                    {
                        Entity.Route = Entity.Name + "/{slug}";
                    }
                }

                if (Entity.Template.IsNullOrEmpty())
                {
                    if (Entity.Type == SectionType.Single)
                    {
                        Entity.Template = Entity.Name + "/Index";
                    }
                    else
                    {
                        Entity.Template = Entity.Name + "/Entry";
                    }
                }
            }
        }
    }
}
