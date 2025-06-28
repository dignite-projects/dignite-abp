
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace Dignite.Cms.Public.Entries
{
    public class GetEntriesInput : PagedResultRequestDto
    {
        public GetEntriesInput()
        {
            MaxResultCount = 20;
        }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string Culture { get; set; }

        [Required]
        public Guid SectionId { get; set; }

        public Guid? EntryTypeId { get; set; }

        public Guid? CreatorId { get; set; }

        public DateTime? StartPublishDate { get; set; }

        public DateTime? ExpiryPublishDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string QueryingByFieldsJson { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<Guid> EntryIds { get; set; }
    }
}
