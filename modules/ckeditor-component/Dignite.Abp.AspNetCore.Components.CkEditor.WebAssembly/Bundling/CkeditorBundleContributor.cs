using Volo.Abp.Bundling;

namespace Dignite.Abp.AspNetCore.Components.CkEditor.WebAssembly.Bundling;

public class CkeditorBundleContributor : IBundleContributor
{
    public void AddScripts(BundleContext context)
    {
        context.Add("_content/Dignite.Abp.AspNetCore.Components.CkEditor/libs/ckeditor5/ckeditor.js");
        context.Add("_content/Dignite.Abp.AspNetCore.Components.CkEditor/libs/ckeditor5/translations/zh-cn.js");
        context.Add("_content/Dignite.Abp.AspNetCore.Components.CkEditor/libs/ckeditor5/ckeditor-blazor.js");
        context.Add("_content/Dignite.Abp.AspNetCore.Components.CkEditor.WebAssembly/libs/ckeditor5/readAppSettingsJson.js");
    }

    public void AddStyles(BundleContext context)
    {
    }
}