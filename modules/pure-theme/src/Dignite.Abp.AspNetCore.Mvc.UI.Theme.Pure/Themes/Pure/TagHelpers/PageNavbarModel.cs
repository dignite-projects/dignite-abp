using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared.Toolbars;
using Volo.Abp.UI.Navigation;

namespace Dignite.Abp.AspNetCore.Mvc.UI.Theme.Pure.Themes.Pure.TagHelpers;
public class PageNavbarModel
{
    public string Title { get; set; }

    public Toolbar Toolbar { get; set; }

    public ApplicationMenu Menu { get; set; }
}
