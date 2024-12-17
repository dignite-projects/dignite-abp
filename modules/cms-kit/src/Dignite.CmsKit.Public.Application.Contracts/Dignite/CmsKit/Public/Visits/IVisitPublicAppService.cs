using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Dignite.CmsKit.Public.Visits;
public interface IVisitPublicAppService : IApplicationService
{
    Task<VisitDto> CreateAsync(string entityType, string entityId, CreateVisitInput input);

    Task<ListResultDto<string>> GetListForUserAsync(string entityType, int skipCount = 0, int maxResultCount = 100);
}
