using Dignite.Cms.Fields;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Dignite.Cms.Admin.Fields
{
    [Authorize(Permissions.CmsAdminPermissions.Field.Default)]
    public class FieldAdminAppService : CmsAdminAppServiceBase, IFieldAdminAppService
    {
        private readonly IFieldRepository  _fieldRepository;
        private readonly FieldManager _fieldManager;
        private readonly IFieldGroupAdminAppService _fieldGroupAdminAppService;

        public FieldAdminAppService( IFieldRepository fieldRepository, FieldManager fieldManager, IFieldGroupAdminAppService fieldGroupAdminAppService)
        {
            _fieldRepository = fieldRepository;
            _fieldManager = fieldManager;
            _fieldGroupAdminAppService = fieldGroupAdminAppService;
        }

        [Authorize(Permissions.CmsAdminPermissions.Field.Create)]
        public async Task<FieldDto> CreateAsync(CreateFieldInput input)
        {
            var entity = await _fieldManager.CreateAsync(
                input.GroupId, 
                input.Name, 
                input.DisplayName, 
                input.Description, 
                input.FormControlName, 
                input.FormConfiguration, 
                CurrentTenant.Id);

            var dto =
                ObjectMapper.Map<Field, FieldDto>(
                    entity
                    );

            return dto;
        }

        [Authorize(Permissions.CmsAdminPermissions.Field.Delete)]
        public async Task DeleteAsync(Guid id)
        {
            await _fieldRepository.DeleteAsync(id);
        }

        [Authorize(Permissions.CmsAdminPermissions.Field.Default)]
        public virtual async Task<FieldDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Field, FieldDto>(
                await _fieldRepository.GetAsync(id)
            );
        }

        [Authorize(Permissions.CmsAdminPermissions.Field.Default)]
        public async Task<PagedResultDto<FieldDto>> GetListAsync(GetFieldsInput input)
        {
            var allGroups = (await _fieldGroupAdminAppService.GetListAsync(new GetFieldGroupsInput())).Items;
            var count = await _fieldRepository.GetCountAsync(input.GroupId, input.Filter);
            var result = await _fieldRepository.GetListAsync(input.GroupId,input.Filter,input.MaxResultCount, input.SkipCount,input.Sorting);
            var dto =
                ObjectMapper.Map<List<Field>, List<FieldDto>>(
                    result
                    );
            foreach (var item in dto)
            {
                item.GroupName = allGroups.FirstOrDefault(g => (Guid?)g.Id == item.GroupId)?.Name;
            }
            return new PagedResultDto<FieldDto>(count, dto);
        }

        [Authorize(Permissions.CmsAdminPermissions.Field.Update)]
        public async Task<FieldDto> UpdateAsync(Guid id, UpdateFieldInput input)
        {
            var entity = await _fieldManager.UpdateAsync(id,
                input.GroupId,
                input.Name,
                input.DisplayName,
                input.Description,
                input.FormControlName,
                input.FormConfiguration);

            var dto =
                ObjectMapper.Map<Field, FieldDto>(
                    entity
                    );

            return dto;
        }
        [Authorize(Permissions.CmsAdminPermissions.Field.Default)]
        public async Task<bool> NameExistsAsync(string name)
        {
            return await _fieldRepository.NameExistsAsync(name);
        }
    }
}
