using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.FieldCustomizing.Forms;

public class FormProviderSelector : IFormProviderSelector, ITransientDependency
{
    protected IEnumerable<IFormProvider> FieldProviders { get; }

    public FormProviderSelector(
        IEnumerable<IFormProvider> fieldProviders)
    {
        FieldProviders = fieldProviders;
    }

    [NotNull]
    public virtual IFormProvider Get([NotNull] string providerName)
    {
        if (!FieldProviders.Any())
        {
            throw new AbpException("No field control provider was registered! At least one provider must be registered to be able to use the field customizing system.");
        }

        var fieldProvider = FieldProviders.SingleOrDefault(fp => fp.Name == providerName);

        if (fieldProvider == null)
            throw new AbpException(
                $"Could not find the field control provider with the name ({providerName}) ."
            );
        else
            return fieldProvider;
    }
}