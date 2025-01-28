using Dignite.Cms.EntityFrameworkCore;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Dignite.Cms.Fields
{
    public class EfCoreFieldGroupRepository : EfCoreRepository<ICmsDbContext, FieldGroup,Guid>, IFieldGroupRepository
    {
        public EfCoreFieldGroupRepository(
            IDbContextProvider<ICmsDbContext> dbContextProvider
            )
            : base(dbContextProvider)
        {
        }
    }
}
