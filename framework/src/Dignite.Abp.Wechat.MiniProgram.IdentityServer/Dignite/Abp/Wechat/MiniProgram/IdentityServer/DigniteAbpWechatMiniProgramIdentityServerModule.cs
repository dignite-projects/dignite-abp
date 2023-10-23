using Dignite.Abp.Wechat.MiniProgram.IdentityServer.Localization;
using Dignite.Abp.Wechat.MiniProgram.Login;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Dignite.Abp.Wechat.MiniProgram.IdentityServer;

[DependsOn(
    typeof(DigniteAbpWechatMiniProgramModule)
)]
public class DigniteAbpWechatMiniProgramIdentityServerModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        //配置微信miniprogram授权登陆
        PreConfigure<IIdentityServerBuilder>(builder =>
        {
            builder.AddExtensionGrantValidator<IdentityServerGrantValidator>();
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<DigniteAbpWechatMiniProgramIdentityServerModule>();
        });

        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Add<DigniteAbpWechatMiniProgramIdentityServerResource>("en")
                .AddVirtualJson("/Dignite/Abp/Wechat/MiniProgram/IdentityServer/Localization/Resources");
        });


        Configure<AbpExceptionLocalizationOptions>(options =>
        {
            options.MapCodeNamespace("Wechat.MiniProgram.IdentityServer", typeof(DigniteAbpWechatMiniProgramIdentityServerResource));
        });

    }
}