using Dignite.Abp.DynamicForms.Components.BlazoriseUI.Components.Textbox;
using Dignite.Abp.DynamicForms.Textbox;
using Shouldly;
using Xunit;

namespace Dignite.Abp.DynamicForms.Components;

public class FormConfigurationComponentSelector_Tests : FieldComponentsTestBase
{
    private readonly IConfigurationComponentSelector _selector;

    public FormConfigurationComponentSelector_Tests()
    {
        _selector = GetRequiredService<IConfigurationComponentSelector>();
    }

    [Fact]
    public void Should_Select_Textbox_Component_Provider()
    {
        _selector.Get(TextboxForm.ProviderName).ShouldBeAssignableTo<TextboxConfigurationComponent>();
    }
}