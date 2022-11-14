using System;
using Dignite.Abp.FieldCustomizing.Localization;
using Microsoft.Extensions.Localization;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.FieldCustomizing.Forms;

public abstract class FormProviderBase : IFormProvider, ITransientDependency
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

    private Type _localizationResource = typeof(AbpFieldCustomizingResource);

    public abstract string Name { get; }

    public abstract string DisplayName { get; }

    public abstract FormType FormType { get; }

    public abstract void Validate(FormValidateArgs args);

    public abstract FormConfigurationBase GetConfiguration(FormConfigurationDictionary fieldConfiguration);

    protected virtual IStringLocalizer CreateLocalizer()
    {
        return StringLocalizerFactory.Create(LocalizationResource);
    }
}