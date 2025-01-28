using Dignite.Cms.Sections;
using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace Dignite.Cms.Admin.Sections
{
    [Serializable]
    public class SectionDto : ExtensibleAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public SectionDto():base()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public SectionType Type { get; set; }

        /// <summary>
        /// Display Name of this section.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Name of this section.
        /// Section Unique Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The default section in the site
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
        public string Route { get; set; }

        /// <summary>
        /// asp.net core mvc Razor Page
        /// </summary>
        public string Template { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ConcurrencyStamp { get; set; }


        public IList<EntryTypeDto> EntryTypes { get; set; }
    }
}
