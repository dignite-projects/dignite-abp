using Volo.Abp.AspNetCore.Mvc.UI.Bundling;

namespace Dignite.Abp.AspNetCore.Components.CkEditor.Server.Bundling;

public class CkeditorScriptContributor : BundleContributor
{
    public override void ConfigureBundle(BundleConfigurationContext context)
    {
        context.Files.Add("/_content/Dignite.Abp.AspNetCore.Components.CkEditor/libs/ckeditor5/ckeditor.js");
        context.Files.Add("/_content/Dignite.Abp.AspNetCore.Components.CkEditor/libs/ckeditor5/translations/zh-cn.js");
        context.Files.Add("/_content/Dignite.Abp.AspNetCore.Components.CkEditor/libs/ckeditor5/ckeditor-blazor.js");
        //context.Files.Add("/_content/Dignite.Abp.AspNetCore.Components.CkEditor.Server/libs/ckeditor5/requestInterceptor.js");
    }

}