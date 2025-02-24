using Dignite.Abp.TenantDomainManagement.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Settings;

namespace Dignite.Abp.TenantDomainManagement.Settings;

public class TenantDomainSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        context.Add(
            new SettingDefinition(
                TenantDomainSettingNames.DomainName,
                "",
                L("DisplayName:Abp.TenantDomain.DomainName"),
                L("Description:Abp.TenantDomain.DomainName"),
                true
                ).WithProviders("T")
        );
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<TenantDomainManagementResource>(name);
    }
}
