using Dignite.Cms.Fields;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Dignite.Cms.Public.Fields
{
    public interface IFieldPublicAppService : IApplicationService
    {
        Task<FieldDto> FindByNameAsync(string name);
    }
}
