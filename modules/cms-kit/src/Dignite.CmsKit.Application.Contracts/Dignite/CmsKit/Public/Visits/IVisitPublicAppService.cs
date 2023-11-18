using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Dignite.CmsKit.Public.Visits;
public interface IVisitPublicAppService : IApplicationService
{
    Task<VisitDto> CreateAsync(string entityType, string entityId, CreateVisitInput input);
}
