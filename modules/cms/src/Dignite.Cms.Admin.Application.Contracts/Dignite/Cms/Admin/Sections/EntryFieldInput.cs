﻿using Dignite.Cms.Sections;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Validation;

namespace Dignite.Cms.Admin.Sections
{
    public class EntryFieldInput
    {
        public EntryFieldInput()
        {
        }

        public EntryFieldInput(Guid fieldId, string displayName, bool required, bool showInList, bool enableSearch)
        {
            FieldId = fieldId;
            DisplayName = displayName;
            Required = required;
            ShowInList = showInList;
            EnableSearch = enableSearch;
        }

        [Required]
        public Guid FieldId { get; set; }


        /// <summary>
        /// Text to override field definition
        /// </summary>
        [Required]
        [DynamicMaxLength(typeof(EntryTypeConsts), nameof(EntryTypeConsts.MaxDisplayNameLength))]
        public string DisplayName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool Required { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool ShowInList { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool EnableSearch { get; set; }
    }
}
