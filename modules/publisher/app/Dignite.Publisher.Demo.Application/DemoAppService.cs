using Dignite.Publisher.Demo.Localization;
using Volo.Abp.Application.Services;

namespace Dignite.Publisher.Demo;

/* Inherit your application services from this class.
 */
public abstract class DemoAppService : ApplicationService
{
    protected DemoAppService()
    {
        LocalizationResource = typeof(DemoResource);
    }
}
