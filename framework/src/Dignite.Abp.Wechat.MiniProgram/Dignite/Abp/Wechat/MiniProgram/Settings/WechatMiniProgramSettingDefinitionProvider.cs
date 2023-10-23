using Dignite.Abp.Wechat.MiniProgram.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Settings;

namespace Dignite.Abp.Wechat.MiniProgram.Settings;

public class WechatMiniProgramSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        var definitions = new SettingDefinition[] {
            new SettingDefinition(
                name:WechatMiniProgramSettings.AppId,
                displayName:L("WechatMpSettings.MiniProgramAppId"),
                isVisibleToClients:false,
                isEncrypted:false),

            new SettingDefinition(
                name:WechatMiniProgramSettings.Secret,
                displayName:L("WechatMpSettings.MiniProgramSecret"),
                isVisibleToClients:false,
                isEncrypted:false),

        };

        context.Add(definitions);
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<DigniteAbpWechatMiniProgramResource>(name);
    }
}