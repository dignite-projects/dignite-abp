using Dignite.Cms.Sections;
using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Dignite.Cms.Public.Sections
{
    public class EntryTypeDto: EntityDto<Guid>
    {
        /// <summary>
        /// Display Name of this entry type.
        /// </summary>
        public virtual string DisplayName { get; set; }

        /// <summary>
        /// Name of this entry type.
        /// Entry Type Unique Name.
        /// </summary>
        public virtual string Name { get; set; }

        public IList<EntryFieldTabDto> FieldTabs { get; set; }
    }
}
