using Dignite.Abp.Data;
using Dignite.Cms.Entries;
using Dignite.Cms.Sections;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Dignite.Cms.Admin.Entries
{
    public class EntryAdminAppService : CmsAdminAppServiceBase, IEntryAdminAppService
    {
        private readonly IEntryRepository _entryRepository;        
        private readonly ISectionRepository _sectionRepository;
        private readonly EntryManager _entryManager;

        public EntryAdminAppService(
            IEntryRepository entryRepository, 
            ISectionRepository sectionRepository,
            EntryManager entryManager)
        {
            _entryRepository = entryRepository;
            _sectionRepository = sectionRepository;
            _entryManager = entryManager;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        [Authorize(Permissions.CmsAdminPermissions.Entry.Create)]
        public async Task<EntryDto> CreateAsync(CreateEntryInput input)
        {
            if (input.InitialVersionId.HasValue)
            {
                var initialEntry = await _entryRepository.GetAsync(input.InitialVersionId.Value, false);
                input.EntryTypeId = initialEntry.EntryTypeId;
            }

            var entry = await _entryManager.CreateAsync(
                input.EntryTypeId,
                input.Culture,
                input.Slug,
                input.PublishTime,
                input.Draft ? EntryStatus.Draft : EntryStatus.Published,
                input.ParentId,
                input.ExtraProperties,
                input.InitialVersionId,
                input.VersionNotes,
                CurrentTenant.Id
                );

            return ObjectMapper.Map<Entry, EntryDto>(entry);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize(Permissions.CmsAdminPermissions.Entry.Update)]
        public async Task<EntryDto> UpdateAsync(Guid id, UpdateEntryInput input)
        {
            var entry = await _entryManager.UpdateAsync(
                id, input.Slug, input.ParentId, input.PublishTime,
                input.Draft ? EntryStatus.Draft : EntryStatus.Published,
                input.ExtraProperties, input.VersionNotes,input.ConcurrencyStamp
                );

            return ObjectMapper.Map<Entry, EntryDto>(entry);
        }

        /// <summary>
        /// Delete entry;
        /// Delete all revisions synchronously;
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Permissions.CmsAdminPermissions.Entry.Delete)]
        public async Task DeleteAsync(Guid id)
        {
            var entry = await _entryRepository.GetAsync(id, false);

            await _entryRepository.DeleteAsync(entry);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize(Permissions.CmsAdminPermissions.Entry.Default)]
        public async Task<PagedResultDto<EntryDto>> GetListAsync(GetEntriesInput input)
        {
            if (input.SectionId == Guid.Empty)
                return new PagedResultDto<EntryDto>(0, new List<EntryDto>());

            var jsonOptions = new JsonSerializerOptions();
            jsonOptions.PropertyNameCaseInsensitive=true;
            List<QueryingByField> queryingByCustomFields = input.QueryingByFieldsJson.IsNullOrEmpty() ? null : JsonSerializer.Deserialize<List<QueryingByField>>(input.QueryingByFieldsJson, jsonOptions);

            

			var count = await _entryRepository.GetCountAsync(input.Culture,input.SectionId,input.EntryTypeId,  input.CreatorId, input.Status, input.StartPublishDate,input.ExpiryPublishDate, queryingByCustomFields);
            if (count == 0)
                return new PagedResultDto<EntryDto>(0, new List<EntryDto>());

            //get entry list
            var result = await _entryRepository.GetListAsync(input.Culture, input.SectionId, input.EntryTypeId, input.CreatorId, input.Status, input.StartPublishDate, input.ExpiryPublishDate, queryingByCustomFields, input.MaxResultCount, input.SkipCount, input.Sorting);
            var dto = ObjectMapper.Map<List<Entry>, List<EntryDto>>(result);


            return new PagedResultDto<EntryDto>(count, dto);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Permissions.CmsAdminPermissions.Entry.Default)]
        public async Task<EntryDto> GetAsync(Guid id)
        {
            var result = await _entryRepository.GetAsync(id, true);
            return ObjectMapper.Map<Entry, EntryDto>(
                result
                );
        }


        [Authorize(Permissions.CmsAdminPermissions.Entry.Default)]
        public async Task<ListResultDto<EntryDto>> GetAllVersionsAsync(Guid id)
        {
            var entry = await _entryRepository.GetAsync(id, false);
            var result = await _entryManager.GetAllVisionsAsync(entry);

            return new ListResultDto<EntryDto>(
                ObjectMapper.Map<List<Entry>, List<EntryDto>>(result)
                );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Permissions.CmsAdminPermissions.Entry.Update)]
        public async Task ActivateAsync(Guid id)
        {
            var entry = await _entryRepository.GetAsync(id, false);
            await _entryManager.ActivateAsync(entry);
        }


        [Authorize(Permissions.CmsAdminPermissions.Entry.Update)]
        public async Task MoveAsync(Guid id, MoveEntryInput input)
        {
            var entry = await _entryRepository.GetAsync(id, false);
            var section = await _sectionRepository.GetAsync(entry.SectionId,false);
            if (section.Type == SectionType.Structure)
            {
                await _entryManager.MoveAsync(entry, input.ParentId, input.Order);
            }
            else
            {
                throw new Volo.Abp.AbpException("Only entries in the structural section can be moved!");
            }
        }

        [Authorize(Permissions.CmsAdminPermissions.Entry.Default)]
        public async Task<bool> SlugExistsAsync(SlugExistsInput input)
        {
            return await _entryRepository.SlugExistsAsync(input.Culture, input.SectionId, input.Slug);
        }

        [Authorize(Permissions.CmsAdminPermissions.Entry.Default)]
        public async Task<bool> CultureExistWithSingleSectionAsync(CultureExistWithSingleSectionInput input)
        {
            return await _entryRepository.CultureExistWithSingleSectionAsync(input.Culture, input.SectionId, input.EntryTypeId);
        }

        [Authorize(Permissions.CmsAdminPermissions.Entry.Default)]
        public async Task<ListResultDto<EntryDto>> GetListByIdsAsync(Guid sectionId, IEnumerable<Guid> ids)
        {
            var result = await _entryRepository.GetListAsync(sectionId,ids);

            return new ListResultDto<EntryDto>(
                ObjectMapper.Map<List<Entry>, List<EntryDto>>(result)
                );
        }

        public async Task<ListResultDto<EntryDto>> GetLocalizedEntriesBySlugAsync(Guid sectionId, string slug)
        {
            var result = await _entryRepository.GetLocalizedEntriesBySlugAsync(sectionId, slug);
            return new ListResultDto<EntryDto>(
                ObjectMapper.Map<List<Entry>, List<EntryDto>>(result)
                );
        }
    }
}
