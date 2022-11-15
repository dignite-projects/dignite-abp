using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.DynamicForms;

public class FormProviderSelector : IFormProviderSelector, ITransientDependency
{
    protected IEnumerable<IFormProvider> FormProviders { get; }

    public FormProviderSelector(
        IEnumerable<IFormProvider> fieldProviders)
    {
        FormProviders = fieldProviders;
    }

    [NotNull]
    public virtual IFormProvider Get([NotNull] string providerName)
    {
        if (!FormProviders.Any())
        {
            throw new AbpException("No form provider was registered! At least one provider must be registered to be able to use the dynamic form system.");
        }

        var formProvider = FormProviders.SingleOrDefault(fp => fp.Name == providerName);

        if (formProvider == null)
            throw new AbpException(
                $"Could not find the form provider with the name ({providerName}) ."
            );
        else
            return formProvider;
    }
}