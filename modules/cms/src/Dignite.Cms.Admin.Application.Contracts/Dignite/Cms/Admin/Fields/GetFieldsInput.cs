using System;
using Volo.Abp.Application.Dtos;

namespace Dignite.Cms.Admin.Fields
{
    public class GetFieldsInput: PagedAndSortedResultRequestDto
    {
        public GetFieldsInput()
        {
            MaxResultCount= 20;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Filter { get; set; }

        public Guid? GroupId { get; set; }
    }
}
