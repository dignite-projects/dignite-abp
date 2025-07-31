using Dignite.Publisher.Posts;
using Volo.Abp.Domain.Entities;

namespace Dignite.Publisher.Admin.Posts;
public abstract class PostAdminDtoBase : PostDtoBase, IHasConcurrencyStamp
{
    public string ConcurrencyStamp { get; set; }
}
