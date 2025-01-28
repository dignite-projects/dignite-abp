using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Services;

namespace Dignite.Cms.Sections
{
    public class EntryTypeManager : DomainService
    {
        protected readonly IEntryTypeRepository _entryTypeRepository;

        public EntryTypeManager(IEntryTypeRepository entryTypeRepository)
        {
            _entryTypeRepository = entryTypeRepository;
        }

        public virtual async Task<EntryType> CreateAsync(Guid sectionId, string displayName, string name, ICollection<EntryFieldTab> fieldTabs)
        {
            await CheckNameExistenceAsync(sectionId, name);

            var entity = new EntryType(
                GuidGenerator.Create(),
                sectionId,
                displayName,
                name,
                fieldTabs,
                CurrentTenant.Id);
            return entity;
        }
        public virtual async Task<EntryType> UpdateAsync(EntryType entryType, string displayName, string name, ICollection<EntryFieldTab> fieldTabs)
        {
            if (!entryType.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
            {
                await CheckNameExistenceAsync(entryType.Id, name);
            }
            entryType.Set(displayName,name,fieldTabs);

			//
			return await _entryTypeRepository.UpdateAsync(entryType);
		}

        protected virtual async Task CheckNameExistenceAsync(Guid sectionId, string name)
        {
            if (await _entryTypeRepository.NameExistsAsync(sectionId, name))
            {
                throw new EntryTypeNameAlreadyExistException(name);
            }
        }
    }
}
