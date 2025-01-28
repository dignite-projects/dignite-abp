using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;

namespace Dignite.Cms.Fields
{
    public class FieldGroup: Entity<Guid>, IMultiTenant
    {
        public FieldGroup(Guid id, string name, Guid? tenantId):base(id)
        {
            Name = name;
            TenantId = tenantId;
        }

        protected FieldGroup()
        {
        }



        public virtual string Name { get; protected set; }

        /// <summary>
        /// TenantId of this section.
        /// </summary>
        public virtual Guid? TenantId { get; protected set; }

        public virtual ICollection<Field> Fields { get; protected set; }

        public virtual void SetName([NotNull]string name)
        {
            this.Name= name;
        }
    }
}
