using Dignite.Abp.Data;
using Dignite.Cms.Entries;
using System;
using System.Text.Json.Serialization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;
using Volo.CmsKit.Users;

namespace Dignite.Cms.Admin.Entries
{
    /// <summary>
    /// Entry
    /// </summary>
    [Serializable]
    public class EntryDto: ExtensibleAuditedEntityDto<Guid>, IHasCustomFields, IHasConcurrencyStamp
    {
        public EntryDto():base()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public Guid SectionId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid EntryTypeId { get; set;}

        /// <summary>
        /// The culture corresponding to the entry
        /// </summary>
        public string Culture { get; set; }

        public string Title { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Slug { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime PublishTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public EntryStatus Status { get; set; }

        #region Section type is a exclusive property of Structure type

        /// <summary>
        /// Parent entry id of the entry;
        /// When it is affiliated with <see cref="Sections.SectionDto.Type"/>=<see cref="Cms.Sections.SectionType.Structure"/>, this value is valid
        /// </summary>
        public Guid? ParentId { get; set; }

        /// <summary>
        /// Order of the entry
        /// When it is affiliated with <see cref="Sections.SectionDto.Type"/>=<see cref="Cms.Sections.SectionType.Structure"/>, this value is valid
        /// </summary>
        public int Order { get; set; }

        #endregion

        #region Version control information for entry


        /// <summary>
        /// The id of the initial entry id;
        /// </summary>
        /// <remarks>
        /// The Id of the initial version is null;
        /// </remarks>
        public Guid? InitialVersionId { get; set; }

        /// <summary>
        /// Whether it is an activated version
        /// </summary>
        public bool IsActivatedVersion { get; set; }

        /// <summary>
        /// Notes on changes to this version
        /// </summary>
        public string VersionNotes { get; set; }

        #endregion


        /// <summary>
        /// 
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public CmsUserDto Author { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}
