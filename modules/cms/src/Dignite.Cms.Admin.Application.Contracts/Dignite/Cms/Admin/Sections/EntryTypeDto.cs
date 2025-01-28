using Dignite.Cms.Sections;
using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Dignite.Cms.Admin.Sections
{
    public class EntryTypeDto: AuditedEntityDto<Guid>
    {
        /// <summary>
        /// Display Name of this entry type.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Name of this entry type.
        /// Entry Type Unique Name.
        /// </summary>
        public string Name { get; set; }

        public IList<EntryFieldTabDto> FieldTabs { get; set; }

    }
}
