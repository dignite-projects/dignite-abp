using System;
using Volo.Abp.Auditing;

namespace Dignite.Cms.Admin.Fields
{
    [Serializable]
    public class FieldDto: Cms.Fields.FieldDto, IAuditedObject
    {
        public FieldDto():base()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public Guid? GroupId { get; set; }

        public string GroupName { get; set; }

        public DateTime CreationTime { get; set; }

        public Guid? CreatorId { get; set; }


        public Guid? LastModifierId { get; set; }

        public DateTime? LastModificationTime { get; set; }

    }
}
