using Dignite.Abp.DynamicForms;
using Dignite.Cms.Fields;
using JetBrains.Annotations;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Validation;

namespace Dignite.Cms.Admin.Fields
{
    public abstract class CreateOrUpdateFieldInputBase
    {
        protected CreateOrUpdateFieldInputBase()
        {
            FormConfiguration=new FormConfigurationDictionary();
        }

        public  Guid? GroupId { get; set; }

        /// <summary>
        /// Display name of this field.
        /// </summary>
        [Required]
        [DynamicMaxLength(typeof(FieldConsts), nameof(FieldConsts.MaxDisplayNameLength))]
        public string DisplayName { get; set; }

        /// <summary>
        /// Unique Name
        /// </summary>
        [Required]
        [DynamicMaxLength(typeof(FieldConsts), nameof(FieldConsts.MaxNameLength))]
        [RegularExpression(FieldConsts.NameRegularExpression)]
        public string Name { get; set; }

        /// <summary>
        /// Description of the field.
        /// </summary>
        [CanBeNull]
        [DynamicMaxLength(typeof(FieldConsts), nameof(FieldConsts.MaxDescriptionLength))]
        public string Description { get; set; }

        [Required]
        [DynamicMaxLength(typeof(FieldConsts), nameof(FieldConsts.MaxFormControlNameLength))]
        public string FormControlName { get; set; }

        [Required]
        public FormConfigurationDictionary FormConfiguration { get; set; }

    }
}
