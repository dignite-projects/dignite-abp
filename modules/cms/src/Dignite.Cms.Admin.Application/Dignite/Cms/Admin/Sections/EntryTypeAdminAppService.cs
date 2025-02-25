using Dignite.Cms.Fields;
using Dignite.Cms.Sections;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Dignite.Cms.Admin.Sections
{
    public class EntryTypeAdminAppService : CmsAdminAppServiceBase, IEntryTypeAdminAppService
    {
        private readonly IEntryTypeRepository _entryTypeRepository;
        private readonly IFieldRepository _fieldRepository;
        private readonly EntryTypeManager _entryTypeManager;

        public EntryTypeAdminAppService(IEntryTypeRepository entryTypeRepository, IFieldRepository fieldRepository, EntryTypeManager entryTypeManager)
        {
            _entryTypeRepository = entryTypeRepository;
            _fieldRepository= fieldRepository;
            _entryTypeManager = entryTypeManager;
        }

        [Authorize(Permissions.CmsAdminPermissions.Section.Create)]
        public async Task<EntryTypeDto> CreateAsync(CreateEntryTypeInput input)
        {
            var entity = await _entryTypeManager.CreateAsync(
                input.SectionId, 
                input.DisplayName, 
                input.Name, 
                input.FieldTabs.Select(ft =>
                    new EntryFieldTab(
                        ft.Name,
                        ft.Fields.Select(f =>
                            new EntryField(
                                f.FieldId,
                                f.DisplayName,
                                f.Required,
                                f.ShowInList,
                                f.EnableSearch
                                )
                            ).ToList()
                        )
                    ).ToList()
                );
            entity = await _entryTypeRepository.InsertAsync( entity );

            return ObjectMapper.Map<EntryType, EntryTypeDto>(entity);
        }

        [Authorize(Permissions.CmsAdminPermissions.Section.Delete)]
        public async Task DeleteAsync(Guid id)
        {
            await _entryTypeRepository.DeleteAsync(id);
        }

        [Authorize(Permissions.CmsAdminPermissions.Section.Default)]
        public async Task<EntryTypeDto> GetAsync(Guid id)
        {
            var entity = await _entryTypeRepository.GetAsync(id);
            var allFields = await _fieldRepository.GetListAsync(
                entity.FieldTabs.SelectMany(f=>f.Fields).Select(f=>f.FieldId)
                );
            var dto = ObjectMapper.Map<EntryType, EntryTypeDto>(entity);
            foreach (var tab in dto.FieldTabs)
            {
                foreach (var ef in tab.Fields)
                {
                    ef.Field = ObjectMapper.Map<Field,FieldDto>( allFields.FirstOrDefault(f => f.Id == ef.FieldId));
                }
            }

            return dto;
        }

        [Authorize(Permissions.CmsAdminPermissions.Section.Default)]
        public async Task<bool> NameExistsAsync(EntryTypeNameExistsInput input)
        {
            return await _entryTypeRepository.NameExistsAsync(input.SectionId, input.Name);
        }

        [Authorize(Permissions.CmsAdminPermissions.Section.Update)]
        public async Task<EntryTypeDto> UpdateAsync(Guid id, UpdateEntryTypeInput input)
        {
            var entity = await _entryTypeRepository.GetAsync(id);
			entity = await _entryTypeManager.UpdateAsync(
				entity,
				input.DisplayName,
                input.Name,
                input.FieldTabs.Select(ft =>
                    new EntryFieldTab(
                        ft.Name,
                        ft.Fields.Select(f =>
                            new EntryField(
                                f.FieldId,
                                f.DisplayName,
                                f.Required,
                                f.ShowInList,
                                f.EnableSearch
                                )
                            ).ToList()
                        )
                    ).ToList()
                );

			return ObjectMapper.Map<EntryType, EntryTypeDto>(entity);
        }
    }
}
