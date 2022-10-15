using Dignite.Abp.FieldCustomizing.Components.BlazoriseUI.Components.Textbox;
using Dignite.Abp.FieldCustomizing.Fields.Textbox;
using Shouldly;
using Xunit;

namespace Dignite.Abp.FieldCustomizing.Components;

public class FieldControlComponentSelector_Tests : FieldComponentsTestBase
{
    private readonly IFieldControlComponentSelector _selector;

    public FieldControlComponentSelector_Tests()
    {
        _selector = GetRequiredService<IFieldControlComponentSelector>();
    }

    [Fact]
    public void Should_Select_Textbox_Component_Provider()
    {
        _selector.Get(TextboxFieldProvider.ProviderName).ShouldBeAssignableTo<TextboxFieldControlComponent>();
    }
}