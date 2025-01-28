using Dignite.Cms.Sections;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Volo.Abp.Application.Dtos;

namespace Dignite.Cms.Public.Sections
{
    [Serializable]
    public class SectionDto : ExtensibleEntityDto<Guid>
    {
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
        /// Is this section a active
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


        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ICollection<EntryTypeDto> EntryTypes { get; set; }
    }
}
