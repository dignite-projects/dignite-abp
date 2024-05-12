using test_Item.Localization;
using Volo.Abp.Application.Services;

namespace test_Item;

/* Inherit your application services from this class.
 */
public abstract class test_ItemAppService : ApplicationService
{
    protected test_ItemAppService()
    {
        LocalizationResource = typeof(test_ItemResource);
    }
}
