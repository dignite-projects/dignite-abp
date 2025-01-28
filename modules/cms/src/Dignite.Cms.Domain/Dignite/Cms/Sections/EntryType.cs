using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Dignite.Cms.Sections
{
    public class EntryType: FullAuditedEntity<Guid>, IMultiTenant
    {
        public EntryType(Guid id, Guid sectionId, string displayName, string name, ICollection<EntryFieldTab> fieldTabs, Guid? tenantId)
            :base(id)
        {
            SectionId = sectionId;
            DisplayName = displayName;
            Name = name;
            FieldTabs = fieldTabs;
            TenantId = tenantId;
        }

        protected EntryType()
        {
            this.FieldTabs=new List<EntryFieldTab>();
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual Guid SectionId { get; protected set; }

        /// <summary>
        /// Display Name of this entry type.
        /// </summary>
        public virtual string DisplayName { get; protected set; }

        /// <summary>
        /// Name of this entry type.
        /// Entry Type Unique Name.
        /// </summary>
        public virtual string Name { get; protected set; }

        public virtual ICollection<EntryFieldTab> FieldTabs { get; protected set; }

        /// <summary>
        /// TenantId of this entry type.
        /// </summary>
        public virtual Guid? TenantId { get; protected set; }

        public virtual void Set(string displayName, string name, ICollection<EntryFieldTab> fieldTabs)
        {
            this.DisplayName = displayName;
            this.Name = name;
            this.FieldTabs = fieldTabs;
        }
	}
}
