using Dignite.Abp.FieldCustomizing.FieldComponents.Components.Textbox;
using Dignite.Abp.FieldCustomizing.Fields.Textbox;
using Shouldly;
using Xunit;

namespace Dignite.Abp.FieldCustomizing.FieldComponents;

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
        _selector.Get(TextboxFieldProvider.ProviderName).ShouldBeAssignableTo<TextboxFieldComponent>();
    }
}