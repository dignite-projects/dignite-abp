using Dignite.Abp.Wechat.MiniProgram.IdentityServer.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Settings;

namespace Dignite.Abp.Wechat.MiniProgram.IdentityServer.Settings;

public class WechatMpSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        var definitions = new SettingDefinition[] {
            new SettingDefinition(
                name:IdentityServerSettings.ClientId,
                displayName:L("IdentityServer.ClientId"),
                isVisibleToClients:false,
                isEncrypted:false).WithProviders("C"),

            new SettingDefinition(
                name:IdentityServerSettings.ClientSecret,
                displayName:L("IdentityServer.ClientSecret"),
                isVisibleToClients:false,
                isEncrypted:false).WithProviders("C")
        };

        context.Add(definitions);
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<DigniteAbpWechatMiniProgramIdentityServerResource>(name);
    }
}