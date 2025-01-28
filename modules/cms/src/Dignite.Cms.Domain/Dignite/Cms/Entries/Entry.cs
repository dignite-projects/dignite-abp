using Dignite.Abp.Data;
using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Dignite.Cms.Entries
{
    /// <summary>
    /// Entry
    /// </summary>
    public class Entry: FullAuditedAggregateRoot<Guid>, IHasCustomFields, IMultiTenant
    {
        protected Entry()
        {
        }

        public Entry(Guid id, Guid sectionId, Guid entryTypeId, string culture, string title, string slug, 
            DateTime publishTime, EntryStatus status, Guid? parentId, int order, 
            Guid? initialVersionId, string versionNotes, Guid? tenantId)
            :base(id)
        {
            SectionId = sectionId;
            EntryTypeId = entryTypeId;
            Culture = culture;
            Title = title;
            Slug = slug;
            PublishTime = publishTime;
            Status = status;
            ParentId = parentId;
            Order = order;
            InitialVersionId = initialVersionId;
            IsActivatedVersion = initialVersionId.HasValue ? false : true;
            VersionNotes = versionNotes;
            TenantId = tenantId;
        }


        /// <summary>
        /// 
        /// </summary>
        public virtual Guid SectionId { get; protected set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual Guid EntryTypeId { get; protected set;}

        /// <summary>
        /// The Culture name corresponding to the entry
        /// </summary>
        public virtual string Culture { get; set; }

        public virtual string Title { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string Slug { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual DateTime PublishTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual EntryStatus Status { get; protected set; }


        #region Section type is a exclusive property of Structure type

        /// <summary>
        /// Parent entry id of the entry;
        /// When it is affiliated with <see cref="Sections.Section.Type"/>=<see cref="Sections.SectionType.Structure"/>, this value is valid
        /// </summary>
        public virtual Guid? ParentId { get; protected set; }

        /// <summary>
        /// Order of the entry
        /// When it is affiliated with <see cref="Sections.Section.Type"/>=<see cref="Sections.SectionType.Structure"/>, this value is valid
        /// </summary>
        public virtual int Order { get; protected set; }

        #endregion

        #region Version control information for entry


        /// <summary>
        /// The id of the initial entry id;
        /// </summary>
        /// <remarks>
        /// The Id of the initial version is null;
        /// </remarks>
        public virtual Guid? InitialVersionId { get; protected set; }

        /// <summary>
        /// Whether it is an activated version
        /// </summary>
        public virtual bool IsActivatedVersion { get; protected set; }

        /// <summary>
        /// Notes on changes to this version
        /// </summary>
        public virtual string VersionNotes { get; set; }

        #endregion


        public virtual Guid? TenantId { get; protected set; }


        public virtual void SetStatus(EntryStatus status)
        {
            this.Status = status;
        }

        public virtual void SetOrder(Guid? parentId,int order)
        {
            this.ParentId = parentId;
            this.Order = order;
        }


        public virtual void SetIsActivatedVersion(bool isActived)
        {
            this.IsActivatedVersion = isActived;
        }
    }
}
