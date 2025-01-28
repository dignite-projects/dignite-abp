using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Dignite.Cms.Admin.Sections
{
    public interface IEntryTypeAdminAppService : ICreateUpdateAppService<EntryTypeDto, Guid, CreateEntryTypeInput, UpdateEntryTypeInput>, IDeleteAppService<Guid>
    {
        Task<EntryTypeDto> GetAsync(Guid id);
        Task<bool> NameExistsAsync(EntryTypeNameExistsInput input);
    }
}
