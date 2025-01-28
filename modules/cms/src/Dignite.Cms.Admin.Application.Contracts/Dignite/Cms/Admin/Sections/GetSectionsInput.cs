using Volo.Abp.Application.Dtos;

namespace Dignite.Cms.Admin.Sections
{
    public class GetSectionsInput: PagedAndSortedResultRequestDto
    {        /// <summary>
        /// 
        /// </summary>
        public string Filter { get; set; }

        public bool? IsActive { get; set; }
    }
}
