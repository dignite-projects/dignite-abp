using Dignite.Abp.Data;
using Dignite.Cms.Fields;
using Dignite.Cms.Sections;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.Domain.Services;

namespace Dignite.Cms.Entries
{
    public class EntryManager : DomainService
    {
        protected readonly ISectionRepository _sectionRepository;
        protected readonly IEntryRepository _entryRepository;
        protected readonly IEntryTypeRepository _entryTypeRepository;
        protected readonly IFieldRepository _fieldRepository;

        public EntryManager(ISectionRepository sectionRepository, IEntryRepository entryRepository, IEntryTypeRepository entryTypeRepository, IFieldRepository fieldRepository)
        {
            _sectionRepository = sectionRepository;
            _entryRepository = entryRepository;
            _entryTypeRepository = entryTypeRepository;
            _fieldRepository = fieldRepository;
        }

        public virtual async Task<Entry> CreateAsync(Guid entryTypeId, string culture, string title, string slug,
            DateTime publishTime, EntryStatus status, Guid? parentId, ExtraPropertyDictionary extraProperties,
            Guid? initialVersionId, string versionNotes,Guid? tenantId)
        {
            var entryType = await _entryTypeRepository.GetAsync(entryTypeId);
            int order;
            if (initialVersionId.HasValue)
            {
                var initialVersionEntry = await _entryRepository.GetAsync(initialVersionId.Value);
                if(!initialVersionEntry.Culture.Equals(culture,StringComparison.CurrentCultureIgnoreCase)
                    || initialVersionEntry.EntryTypeId!=entryTypeId
                    || initialVersionEntry.ParentId!=parentId)
                {
                    throw new EntryInformationInconsistentException(culture,entryTypeId,parentId);
                }
                order = initialVersionEntry.Order;
            }
            else
            {
                await CheckCultureExistenceAsync(culture, entryType);
                await CheckSlugExistenceAsync(culture, entryType.SectionId, slug);
                order = (await _entryRepository.GetMaxOrderAsync(culture, entryType.SectionId, parentId)) + 1;
            }
            await CheckExtraPropertiesAsync(entryType,extraProperties);

            var entry = new Entry(
                GuidGenerator.Create(),
                entryType.SectionId,
                entryTypeId,
                culture,
                title,
                slug,
                publishTime,
                status,
                parentId,
                order,
                initialVersionId,
                versionNotes,
                tenantId);

            foreach (var item in extraProperties)
            {
                entry.SetField(item.Key, item.Value);
            }


            //          
            entry =  await _entryRepository.InsertAsync(entry);

            //
            if (initialVersionId.HasValue && status == EntryStatus.Published)
            {
                await ActivateAsync(entry);
            }

            return entry;
        }

        public virtual async Task<Entry> UpdateAsync(
            Guid id, string title, string slug,Guid? parentId,
            DateTime publishTime, EntryStatus status, ExtraPropertyDictionary extraProperties,
            string versionNotes,
            string concurrencyStamp)
        {
            var entry = await _entryRepository.GetAsync(id, false);
            var section = await _sectionRepository.GetAsync(entry.SectionId);
            var entryType = section.EntryTypes.Single(et => et.Id == entry.EntryTypeId);
            await CheckExtraPropertiesAsync(entryType, extraProperties);
            entry.SetConcurrencyStampIfNotNull(concurrencyStamp);

            //
            entry.Title = title;
            entry.Slug = slug;
            entry.PublishTime = publishTime;
            entry.SetStatus(status);
            entry.VersionNotes = versionNotes;

            entry.ExtraProperties.Clear();
            foreach (var item in extraProperties)
            {
                entry.SetField(item.Key, item.Value);
            }

            //
            entry = await _entryRepository.UpdateAsync(entry);
            if (entry.InitialVersionId.HasValue && !entry.IsActivatedVersion && status == EntryStatus.Published)
            {
                await ActivateAsync(entry);
            }

            if (section.Type== SectionType.Structure && parentId != entry.ParentId)
            {
                await MoveAsync(entry, parentId, entry.Order);
            }


            return entry;
        }

        public virtual async Task<List<Entry>> GetAllVisionsAsync(Entry entry)
        {
            var initialVersionId = entry.InitialVersionId.HasValue ? entry.InitialVersionId.Value : entry.Id;
            return await _entryRepository.GetVisionListAsync(initialVersionId);
        }

        public virtual async Task ActivateAsync(Entry entry)
        {
            if (!entry.IsActivatedVersion)
            {
                var visions = await GetAllVisionsAsync(entry);
                foreach (var item in visions)
                {
                    if (item.IsActivatedVersion)
                    {
                        item.SetIsActivatedVersion(false);
                    }
                }
                entry.SetIsActivatedVersion(true);
            }
        }

        public virtual async Task MoveAsync(Entry entry, Guid? parentId,int order)
        {
            if (entry.Order != order)
            {
                var allEntries = (await _entryRepository.GetListAsync(entry.Culture, entry.SectionId))
                    .Where(e => e.ParentId == parentId && e.Order >= order);
                foreach (var item in allEntries)
                {
                    item.SetOrder(item.ParentId, item.Order + 1);
                }
                entry.SetOrder(parentId, order);
            }
        }
        protected virtual async Task CheckCultureExistenceAsync(string culture, EntryType entryType)
        {
            var section = await _sectionRepository.GetAsync(entryType.SectionId);
            if (section.Type == SectionType.Single)
            {
                if (await _entryRepository.CultureExistWithSingleSectionAsync(culture, entryType.SectionId, entryType.Id))
                {
                    throw new EntryCultureAlreadyExistException(culture, entryType.Id);
                }
            }
        }
        protected virtual async Task CheckSlugExistenceAsync(string culture,Guid sectionId,  string slug)
        {
            if (await _entryRepository.SlugExistsAsync(culture,sectionId, slug))
            {
                throw new EntrySlugAlreadyExistException(culture, slug);
            }
        }
        protected virtual async Task CheckExtraPropertiesAsync(EntryType entryype, ExtraPropertyDictionary extraProperties)
        {
            var fields = await _fieldRepository.GetListAsync( 
                entryype.FieldTabs
                        .SelectMany(ft => ft.Fields)
                        .Select(ef => ef.FieldId)
                );
            foreach (var fieldName in extraProperties.Keys.Except(fields.Select(f => f.Name)))
            {
                extraProperties.Remove(fieldName);
            }
        }
    }
}
