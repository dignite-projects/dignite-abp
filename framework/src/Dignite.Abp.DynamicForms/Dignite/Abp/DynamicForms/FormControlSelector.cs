using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.DynamicForms;

public class FormControlSelector : IFormControlSelector, ITransientDependency
{
    protected IEnumerable<IFormControl> Forms { get; }

    public FormControlSelector(
        IEnumerable<IFormControl> forms)
    {
        Forms = forms;
    }

    [NotNull]
    public virtual IFormControl Get([NotNull] string name)
    {
        var form = Forms.SingleOrDefault(fp => fp.Name == name);

        if (form == null)
            throw new AbpException(
                $"Could not find the form control with the name ({name}) ."
            );
        else
            return form;
    }
}