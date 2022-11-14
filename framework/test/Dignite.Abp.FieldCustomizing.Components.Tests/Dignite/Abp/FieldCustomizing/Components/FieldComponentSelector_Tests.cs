using Dignite.Abp.FieldCustomizing.Components.BlazoriseUI.Components.Textbox;
using Dignite.Abp.FieldCustomizing.Forms.Textbox;
using Shouldly;
using Xunit;

namespace Dignite.Abp.FieldCustomizing.Components;

public class FieldComponentSelector_Tests : FieldComponentsTestBase
{
    private readonly IFieldComponentSelector _selector;

    public FieldComponentSelector_Tests()
    {
        _selector = GetRequiredService<IFieldComponentSelector>();
    }

    [Fact]
    public void Should_Select_Textbox_Component_Provider()
    {
        _selector.Get(TextboxFormProvider.ProviderName).ShouldBeAssignableTo<TextboxFieldComponent>();
    }
}