using Dignite.Abp.FieldCustomizing.FieldComponents.Components.Textbox;
using Dignite.Abp.FieldCustomizing.Fields.Textbox;
using Shouldly;
using Xunit;

namespace Dignite.Abp.FieldCustomizing.FieldComponents;

public class FieldConfigurationComponentSelector_Tests : FieldComponentsTestBase
{
    private readonly IFieldConfigurationComponentSelector _selector;

    public FieldConfigurationComponentSelector_Tests()
    {
        _selector = GetRequiredService<IFieldConfigurationComponentSelector>();
    }

    [Fact]
    public void Should_Select_Textbox_Component_Provider()
    {
        _selector.Get(TextboxFieldProvider.ProviderName).ShouldBeAssignableTo<TextboxFieldConfigurationComponent>();
    }
}