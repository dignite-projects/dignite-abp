using Dignite.Abp.DynamicForms;
using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Dignite.Cms.Fields
{
    public class FieldManager : DomainService
    {
        protected readonly IFieldRepository _fieldRepository;

        public FieldManager(IFieldRepository fieldRepository)
        {
            _fieldRepository = fieldRepository;
        }
        public async Task<Field> CreateAsync(Guid? groupId, string name, string displayName, string description, string formControlName, FormConfigurationDictionary formConfiguration, Guid? tenantId)
        {
            await CheckNameExistenceAsync(name);
            var entity = new Field(
                GuidGenerator.Create(),
                groupId,
                name,
                displayName,
                description,
                formControlName,
                formConfiguration,
                tenantId);
            return await _fieldRepository.InsertAsync(entity);
        }
        public async Task<Field> UpdateAsync(Guid id, Guid? groupId, string name, string displayName, string description, string formControlName, FormConfigurationDictionary formConfiguration)
        {
            var entity = await _fieldRepository.GetAsync(id, false);
            if (!entity.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
            {
                await CheckNameExistenceAsync(name);
            }
            entity.SetDisplayName(displayName);
            entity.SetName(name);
            entity.SetDescription(description);
            entity.SetFormControlName(formControlName);
            entity.SetGroupId(groupId);
            entity.SetFormConfigurationDictionary(formConfiguration);
            return await _fieldRepository.UpdateAsync(entity);
        }


        protected virtual async Task CheckNameExistenceAsync(string name)
        {
            if (await _fieldRepository.NameExistsAsync(name))
            {
                throw new FieldNameAlreadyExistException(name);
            }
        }
    }
}
