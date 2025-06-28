using Dignite.Cms.Entries;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Validation;

namespace Dignite.Cms.Public.Entries
{
    public class FindBySlugInput
    {
        [Required]
        public Guid SectionId { get; set; }

        /// <summary>
        /// The Culture corresponding to the entry
        /// </summary>
        [Required]
        [DynamicMaxLength(typeof(EntryConsts), nameof(EntryConsts.MaxCultureLength))]
        public string Culture { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        [DynamicMaxLength(typeof(EntryConsts), nameof(EntryConsts.MaxSlugLength))]
        [RegularExpression(EntryConsts.SlugRegularExpression)]
        public string Slug { get; set; }
    }
}
