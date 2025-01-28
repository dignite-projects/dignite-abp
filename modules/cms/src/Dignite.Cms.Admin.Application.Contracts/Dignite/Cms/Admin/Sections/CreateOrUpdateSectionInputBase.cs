using Dignite.Cms.Sections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.ObjectExtending;
using Volo.Abp.Validation;

namespace Dignite.Cms.Admin.Sections
{
    public abstract class CreateOrUpdateSectionInputBase: ExtensibleObject
    {
        protected CreateOrUpdateSectionInputBase()
        {
            Type = SectionType.Single;
            IsActive = true;
        }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public SectionType Type { get; set; }

        /// <summary>
        /// Display Name of this section.
        /// </summary>
        [Required]
        [DynamicMaxLength(typeof(SectionConsts), nameof(SectionConsts.MaxDisplayNameLength))]
        public string DisplayName { get; set; }

        /// <summary>
        /// Name of this section.
        /// Section Unique Name.
        /// </summary>
        [Required]
        [DynamicMaxLength(typeof(SectionConsts), nameof(SectionConsts.MaxNameLength))]
        [RegularExpression(SectionConsts.NameRegularExpression)]
        public string Name { get; set; }

        /// <summary>
        /// The default section in the site;
        /// Only the single type of section can be set as the default section
        /// </summary>
        public bool IsDefault { get; set; }

        /// <summary>
        /// Is this section active
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Routing format for entry page;
        /// </summary>
        /// <example>
        /// Route with formatting parameters:
        /// 1./news
        /// 2./news/{publishTime:yyyy-M}/{slug}
        /// </example>
        /// <remarks>
        /// 1.If the section is not a single type, {slug} must be included in the route;
        /// 2.Route parameters must be public properties in the entry;
        /// 3.Routing parameters support formatting
        /// </remarks>
        [DynamicMaxLength(typeof(SectionConsts), nameof(SectionConsts.MaxPageRouteLength))]
        [RegularExpression(SectionConsts.PageRouteRegularExpression)]
        public string Route { get; set; }

        /// <summary>
        /// asp.net core mvc Razor Page
        /// </summary>
        [DynamicMaxLength(typeof(SectionConsts), nameof(SectionConsts.MaxPagetemplateLength))]
        [RegularExpression(SectionConsts.PageTemplateRegularExpression)]
        public string Template { get; set; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // TODO: Here the verify route cannot start with "/en/,"/zh-cn/" or any other language.
            return base.Validate(validationContext);
        }
    }
}
