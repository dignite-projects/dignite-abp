using System;
using System.Threading.Tasks;
using Dignite.Abp.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared.Toolbars;
using Volo.Abp.UI.Navigation;

namespace Dignite.Abp.AspNetCore.Mvc.UI.Theme.Pure.Themes.Pure.TagHelpers;
public class PageNavbarTagHelper : TagHelper
{
    /// <summary>
    /// Page Title
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string MenuName { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string ToolbarName { get; set; }

    /// <summary>
    /// The name or path of the view that is rendered to the response.
    /// </summary>
    public string PartialName { get; set; }


    private readonly IRazorPartialRenderer _renderer;

    private readonly IMenuManager _menuManager;

    private readonly IToolbarManager _toolbarManager;

    public PageNavbarTagHelper(
        IRazorPartialRenderer renderer,
        IMenuManager menuManager,
        IToolbarManager toolbarManager
        )
    {
        _renderer = renderer;
        _menuManager = menuManager;
        _toolbarManager = toolbarManager;
    }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var model = new PageNavbarModel();
        model.Title = Title;
        model.Menu = MenuName.IsNullOrEmpty()? new ApplicationMenu("EmptyMenuName") : await _menuManager.GetAsync(MenuName);
        model.Toolbar = ToolbarName.IsNullOrEmpty() ? new Toolbar("EmptyToolbarName") : await _toolbarManager.GetAsync(ToolbarName);

        var body = await _renderer.RenderAsync(
            PartialName.IsNullOrEmpty() ? "PageNavbar" : PartialName,
            model);

        output.TagName = null;
        output.Content.SetHtmlContent(body);
        output.Attributes.Clear();
    }
}
