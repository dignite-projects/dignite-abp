using Dignite.Abp.DynamicForms.Components;
using Dignite.Abp.DynamicForms.Components.BlazoriseUI.Components.Textbox;
using Dignite.Abp.DynamicForms.Textbox;
using Shouldly;
using Xunit;

namespace Dignite.Abp.DynamicForms.Components;

public class FormComponentSelector_Tests : FieldComponentsTestBase
{
    private readonly IFormComponentSelector _selector;

    public FormComponentSelector_Tests()
    {
        _selector = GetRequiredService<IFormComponentSelector>();
    }

    [Fact]
    public void Should_Select_Textbox_Component_Provider()
    {
        _selector.Get(TextboxForm.TextboxFormName).ShouldBeAssignableTo<TextboxFormComponent>();
    }
}