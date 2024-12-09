using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Dignite.CmsKit.Public.MarkedItems;
public interface IMarkedItemPublicAppService : IApplicationService
{
    Task<ListResultDto<string>> GetListForUserAsync([NotNull]string entityType);
}
