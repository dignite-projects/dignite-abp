using Dignite.Abp.Wechat.OfficialAccount.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Settings;

namespace Dignite.Abp.Wechat.OfficialAccount.Settings;

public class WechatOfficialAccountSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        var definitions = new SettingDefinition[] {
            new SettingDefinition(
                name:WechatOfficialAccountSettings.AppId,
                displayName:L("DigniteWechatMpSettings.MpAppId"),
                isVisibleToClients:false,
                isEncrypted:false),

            new SettingDefinition(
                name:WechatOfficialAccountSettings.Secret,
                displayName:L("DigniteWechatMpSettings.MpSecret"),
                isVisibleToClients:false,
                isEncrypted:false)
        };

        context.Add(definitions);
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<DigniteAbpWechatOfficialAccountResource>(name);
    }
}