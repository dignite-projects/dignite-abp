using test_Item.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace test_Item.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class test_ItemController : AbpControllerBase
{
    protected test_ItemController()
    {
        LocalizationResource = typeof(test_ItemResource);
    }
}
