using Dignite.Cms.Fields;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Dignite.Cms.Admin.Fields
{
    public class FieldGroupAdminAppService : CmsAdminAppServiceBase, IFieldGroupAdminAppService
    {
        private readonly IFieldGroupRepository _fieldGroupRepository;

        public FieldGroupAdminAppService(IFieldGroupRepository fieldGroupRepository)
        {
            _fieldGroupRepository = fieldGroupRepository;
        }

        [Authorize(Permissions.CmsAdminPermissions.Field.Create)]
        public async Task<FieldGroupDto> CreateAsync(CreateOrUpdateFieldGroupInput input)
        {
            var entity = new FieldGroup(GuidGenerator.Create(),input.Name,CurrentTenant.Id);
            await _fieldGroupRepository.InsertAsync(entity);

            var dto =
                ObjectMapper.Map<FieldGroup, FieldGroupDto>(
                    entity
                    );

            return dto;
        }

        [Authorize(Permissions.CmsAdminPermissions.Field.Delete)]
        public async Task DeleteAsync(Guid id)
        {
            await _fieldGroupRepository.DeleteAsync(id);
        }

        [Authorize(Permissions.CmsAdminPermissions.Field.Default)]
        public async Task<FieldGroupDto> GetAsync(Guid id)
        {
            var entity = await _fieldGroupRepository.GetAsync(id);
            var dto =
                ObjectMapper.Map<FieldGroup, FieldGroupDto>(
                    entity
                    );

            return dto;
        }

        [Authorize(Permissions.CmsAdminPermissions.Field.Default)]
        public async Task<PagedResultDto<FieldGroupDto>> GetListAsync(GetFieldGroupsInput input)
        {
            var result = await _fieldGroupRepository.GetListAsync();
            var dto =
                ObjectMapper.Map<List<FieldGroup>, List<FieldGroupDto>>(
                    result
                    );

            return new PagedResultDto<FieldGroupDto>(result.Count, dto);
        }

        [Authorize(Permissions.CmsAdminPermissions.Field.Update)]
        public async Task<FieldGroupDto> UpdateAsync(Guid id, CreateOrUpdateFieldGroupInput input)
        {
            var entity = await _fieldGroupRepository.GetAsync(id,false);
            entity.SetName(input.Name);
            await _fieldGroupRepository.UpdateAsync(entity);

            var dto =
                ObjectMapper.Map<FieldGroup, FieldGroupDto>(
                    entity
                    );

            return dto;
        }
    }
}
