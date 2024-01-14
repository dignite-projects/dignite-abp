using System;
using Dignite.Abp.DynamicForms.Localization;
using Microsoft.Extensions.Localization;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.DynamicForms;

/// <summary>
/// Basic class of form control
/// </summary>
public abstract class FormControlBase : IFormControl, ITransientDependency
{
    public IAbpLazyServiceProvider LazyServiceProvider { get; set; }

    protected IStringLocalizerFactory StringLocalizerFactory => LazyServiceProvider.LazyGetRequiredService<IStringLocalizerFactory>();

    protected IStringLocalizer L {
        get {
            if (_localizer == null)
            {
                _localizer = CreateLocalizer();
            }

            return _localizer;
        }
    }

    private IStringLocalizer _localizer;

    protected Type LocalizationResource {
        get => _localizationResource;
        set {
            _localizationResource = value;
            _localizer = null;
        }
    }

    private Type _localizationResource = typeof(DigniteAbpDynamicFormsResource);

    public abstract string Name { get; }

    public abstract string DisplayName { get; }

    public abstract void Validate(FormControlValidateArgs args);

    public abstract FormConfigurationBase GetConfiguration(FormConfigurationDictionary configuration);

    protected virtual IStringLocalizer CreateLocalizer()
    {
        return StringLocalizerFactory.Create(LocalizationResource);
    }
}