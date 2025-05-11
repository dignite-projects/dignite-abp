using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Dignite.Cms.Admin.Entries
{
    public interface IEntryAdminAppService
    : ICrudAppService<
        EntryDto,
        Guid,
        GetEntriesInput,
        CreateEntryInput,
        UpdateEntryInput>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ListResultDto<EntryDto>> GetAllVersionsAsync(Guid id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task ActivateAsync(Guid id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task MoveAsync(Guid id, MoveEntryInput input);

        Task<ListResultDto<EntryDto>> GetLocalizedEntriesBySlugAsync(Guid sectionId, string slug);

        Task<bool> SlugExistsAsync(SlugExistsInput input);

        /// <summary>
        /// When the section is of type Single and an entry already exists under the specified entryTypeId, no new entries are allowed to be created.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<bool> CultureExistWithSingleSectionAsync(CultureExistWithSingleSectionInput input);

        Task<ListResultDto<EntryDto>> GetListByIdsAsync(Guid sectionId, IEnumerable<Guid> ids);
    }
}
