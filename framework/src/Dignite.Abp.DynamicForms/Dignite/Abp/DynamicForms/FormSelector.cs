using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.DynamicForms;

public class FormSelector : IFormSelector, ITransientDependency
{
    protected IEnumerable<IForm> Forms { get; }

    public FormSelector(
        IEnumerable<IForm> forms)
    {
        Forms = forms;
    }

    [NotNull]
    public virtual IForm Get([NotNull] string name)
    {
        var form = Forms.SingleOrDefault(fp => fp.Name == name);

        if (form == null)
            throw new AbpException(
                $"Could not find the form with the name ({name}) ."
            );
        else
            return form;
    }
}