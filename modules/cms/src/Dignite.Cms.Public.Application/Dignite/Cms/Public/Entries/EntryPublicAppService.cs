using Dignite.Abp.Data;
using Dignite.Cms.Entries;
using Dignite.Cms.Public.Sections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Dignite.Cms.Public.Entries
{
    public class EntryPublicAppService : CmsPublicAppService, IEntryPublicAppService
    {
        private readonly IEntryRepository _entryRepository;
        private readonly ISectionPublicAppService _sectionPublicAppService;

        public EntryPublicAppService(IEntryRepository entryRepository, ISectionPublicAppService sectionPublicAppService)
        {
            _entryRepository = entryRepository;
            _sectionPublicAppService= sectionPublicAppService;
        }

        public async Task<EntryDto> FindBySlugAsync(FindBySlugInput input)
        {
            var entry = await _entryRepository.FindBySlugAsync(input.Culture,input.SectionId,input.Slug);
            return ObjectMapper.Map<Entry, EntryDto>(entry);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<EntryDto> FindPrevAsync(Guid id)
        {
            var entry = await _entryRepository.FindPrevAsync(id);
            return ObjectMapper.Map<Entry, EntryDto>(entry);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<EntryDto> FindNextAsync(Guid id)
        {
            var entry = await _entryRepository.FindNextAsync(id);
            return ObjectMapper.Map<Entry, EntryDto>(entry);
        }

        public async Task<EntryDto> GetAsync(Guid id)
        {
            var entry = await _entryRepository.GetAsync(id);
            return ObjectMapper.Map<Entry, EntryDto>(entry);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<EntryDto>> GetListAsync(GetEntriesInput input)
        {
            int count = 0;
            List<Entry> result = new List<Entry>();
            var section = await _sectionPublicAppService.GetAsync(input.SectionId);
            
            if (input.EntryIds == null)
            {
                if (section.Type == Cms.Sections.SectionType.Single)
                {
                    result = (await _entryRepository.GetListAsync(input.Culture, input.SectionId, input.EntryTypeId, null, EntryStatus.Published, null, null, null, null, 100, 0))
                        .OrderBy(e => e.Order)
                        .ToList();
                    count = result.Count;
                }
                else if (section.Type == Cms.Sections.SectionType.Structure)
                {
                    result = (await _entryRepository.GetListAsync(input.Culture, input.SectionId, input.EntryTypeId, null, EntryStatus.Published, null, null, null, null, 1000, 0))
                        .OrderBy(e=>e.Order)
                        .ToList();
                    count = result.Count;
                }
                else
                {
                    List<QueryingByField> queryingByCustomFields = input.QueryingByFieldsJson.IsNullOrEmpty() ? null : JsonSerializer.Deserialize<List<QueryingByField>>(input.QueryingByFieldsJson);
                    count = await _entryRepository.GetCountAsync(input.Culture, input.SectionId, input.EntryTypeId, input.CreatorId, EntryStatus.Published, input.Filter, input.StartPublishDate, input.ExpiryPublishDate, queryingByCustomFields);
                    if (count == 0)
                    {
                        return new PagedResultDto<EntryDto>(0, new List<EntryDto>());
                    }
                    result = await _entryRepository.GetListAsync(
                            input.Culture,
                            input.SectionId,
                            input.EntryTypeId,
                            input.CreatorId,
                            EntryStatus.Published,
                            input.Filter,
                            input.StartPublishDate,
                            input.ExpiryPublishDate,
                            queryingByCustomFields,
                            input.MaxResultCount,
                            input.SkipCount
                            );
                }
            }
            else
            {
                if (input.EntryIds.Any())
                {
                    result = await _entryRepository.GetListAsync(section.Id, input.EntryIds);
                    count = result.Count;   
                }
                else
                {
                    result=new List<Entry>();
                }
            }

            //
            var dto = ObjectMapper.Map<List<Entry>, List<EntryDto>>(result);


            //Remove custom fields that don't show up in the list
            foreach (var item in dto)
            {
                var onListFields = section.EntryTypes
                    .SingleOrDefault(et => et.Id == item.EntryTypeId)?.FieldTabs
                    .SelectMany(ft => ft.Fields.Where(f => f.ShowOnList));
                if (onListFields == null)
                {
                    item.ExtraProperties.Clear();
                }
                else
                {
                    item.ExtraProperties.RemoveAll(ep => !onListFields.Select(f => f.Field.Name).Contains(ep.Key));
                }
            }

            return new PagedResultDto<EntryDto>(count, dto);
        }
    }
}
