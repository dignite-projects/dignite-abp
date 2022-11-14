using Dignite.Abp.FieldCustomizing.Components.BlazoriseUI.Components.Textbox;
using Dignite.Abp.FieldCustomizing.Forms.Textbox;
using Shouldly;
using Xunit;

namespace Dignite.Abp.FieldCustomizing.Components;

public class FieldControlComponentSelector_Tests : FieldComponentsTestBase
{
    private readonly IFieldFormComponentSelector _selector;

    public FieldControlComponentSelector_Tests()
    {
        _selector = GetRequiredService<IFieldFormComponentSelector>();
    }

    [Fact]
    public void Should_Select_Textbox_Component_Provider()
    {
        _selector.Get(TextboxFormProvider.ProviderName).ShouldBeAssignableTo<TextboxFieldFormComponent>();
    }
}