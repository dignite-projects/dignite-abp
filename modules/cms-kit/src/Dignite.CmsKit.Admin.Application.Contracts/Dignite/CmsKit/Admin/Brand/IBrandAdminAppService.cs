using System.Threading.Tasks;

namespace Dignite.CmsKit.Admin.Brand;

public interface IBrandAdminAppService
{
    Task UpdateAsync(UpdateBrandInput input);
}

