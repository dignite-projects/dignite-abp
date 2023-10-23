using Dignite.Abp.Wechat.OfficialAccount.IdentityServer.Localization;
using Dignite.Abp.Wechat.OfficialAccount.WebApp;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Dignite.Abp.Wechat.OfficialAccount.IdentityServer;

[DependsOn(
    typeof(DigniteAbpWechatOfficialAccountModule)
)]
public class DigniteAbpWechatOfficialAccountIdentityServerModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        //配置微信webapp授权登陆
        PreConfigure<IIdentityServerBuilder>(builder =>
        {
            builder.AddExtensionGrantValidator<IdentityServerGrantValidator>();
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<DigniteAbpWechatOfficialAccountIdentityServerModule>();
        });

        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Add<DigniteAbpWechatOfficialAccountIdentityServerResource>("en")
                .AddVirtualJson("/Dignite/Abp/Wechat/OfficialAccount/IdentityServer/Localization/Resources");
        });


        Configure<AbpExceptionLocalizationOptions>(options =>
        {
            options.MapCodeNamespace("Wechat.OfficialAccount.IdentityServer", typeof(DigniteAbpWechatOfficialAccountIdentityServerResource));
        });

    }
}