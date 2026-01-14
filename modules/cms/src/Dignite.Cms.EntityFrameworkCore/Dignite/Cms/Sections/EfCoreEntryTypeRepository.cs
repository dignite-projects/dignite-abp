using Dignite.Cms.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Dignite.Cms.Sections
{
    public class EfCoreEntryTypeRepository : EfCoreRepository<ICmsDbContext, EntryType,Guid>, IEntryTypeRepository
    {

        public EfCoreEntryTypeRepository(
            IDbContextProvider<ICmsDbContext> dbContextProvider
            )
            : base(dbContextProvider)
        {
        }


        public async Task<bool> NameExistsAsync(Guid sectionId, string name, CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync()).AsNoTracking()
                .Where(s=>s.SectionId==sectionId)
                .AnyAsync(s => s.Name == name, GetCancellationToken(cancellationToken));
        }
    }
}
