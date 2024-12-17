using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Dignite.CmsKit.Brand;
public interface IBrandAppService: IApplicationService
{
    Task<BrandDto> GetAsync();
}
