
using Dignite.Cms.Entries;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace Dignite.Cms.Admin.Entries
{
    public class GetEntriesInput : PagedAndSortedResultRequestDto
    {
        public GetEntriesInput()
        {
            MaxResultCount = 50;
        }


        [Required]
        public string Culture { get; set; }

        [Required]
        public Guid SectionId { get; set; }

        public Guid? EntryTypeId { get; set; }

        public DateTime? StartPublishDate { get; set; }

        public DateTime? ExpiryPublishDate { get; set; }

        public string Filter { get; set; }

        public EntryStatus? Status { get; set; }

        public Guid? CreatorId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string QueryingByFieldsJson { get; set; }
    }
}
