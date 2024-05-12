using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace test_Item;

[Dependency(ReplaceServices = true)]
public class test_ItemBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "test_Item";
}
