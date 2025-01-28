using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Dignite.Cms.Public.Entries
{
    public interface IEntryPublicAppService : IReadOnlyAppService<EntryDto, Guid, GetEntriesInput>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<EntryDto> FindBySlugAsync(FindBySlugInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<EntryDto> FindPrevAsync(Guid id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<EntryDto> FindNextAsync(Guid id);
    }
}
