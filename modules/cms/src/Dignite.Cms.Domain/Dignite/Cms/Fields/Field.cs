using Dignite.Abp.DynamicForms;
using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Dignite.Cms.Fields
{
    public class Field: FullAuditedEntity<Guid>, IMultiTenant
    {
        protected Field() { }

        public Field(Guid id, Guid? groupId, string name, string displayName, string description, string formControlName, FormConfigurationDictionary formConfiguration, Guid? tenantId)
            :base(id)
        {
            SetGroupId(groupId);
            Name = name;
            DisplayName = displayName;
            Description = description;
            FormControlName = formControlName;
            FormConfiguration = formConfiguration;
            TenantId = tenantId;
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual Guid? GroupId { get; protected set; }

        /// <summary>
        /// Field Unique Name
        /// </summary>
        public virtual string Name { get; protected set; }


        /// <summary>
        /// 
        /// </summary>
        public virtual string DisplayName { get; protected set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string Description { get; protected set; }

        /// <summary>
        /// Field <see cref="IFormControl.Name"/>
        /// </summary>
        public virtual string FormControlName { get; protected set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual FormConfigurationDictionary FormConfiguration { get; set; }
        public Guid? TenantId { get; set; }
        public virtual void SetGroupId(Guid? groupId)
        {
            this.GroupId= groupId.HasValue ? (groupId.Value == Guid.Empty ? null : groupId.Value) : null;
        }
        public virtual void SetName(string name)
        {
            this.Name = name;
        }
        public virtual void SetDisplayName(string displayName)
        {
            this.DisplayName = displayName;
        }
        public virtual void SetDescription(string description)
        {
            this.Description = description;
        }
        public virtual void SetFormControlName(string formControlName)
        {
            this.FormControlName = formControlName;
        }
        public virtual void SetFormConfigurationDictionary(FormConfigurationDictionary formConfiguration)
        {
            this.FormConfiguration = formConfiguration;
        }
    }
}
