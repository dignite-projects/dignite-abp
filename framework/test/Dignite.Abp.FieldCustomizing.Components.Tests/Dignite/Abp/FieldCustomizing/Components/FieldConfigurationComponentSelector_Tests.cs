using Dignite.Abp.FieldCustomizing.Components.BlazoriseUI.Components.Textbox;
using Dignite.Abp.FieldCustomizing.Fields.Textbox;
using Shouldly;
using Xunit;

namespace Dignite.Abp.FieldCustomizing.Components;

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