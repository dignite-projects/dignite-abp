using Dignite.Abp.DynamicForms;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Dignite.Cms.Admin.DynamicForms
{
    public class FormAdminAppService : CmsAdminAppServiceBase, IFormAdminAppService
    {
        private readonly IEnumerable<IFormControl> _formControls;

        public FormAdminAppService(IEnumerable<IFormControl> formControls)
        {
            _formControls = formControls;
        }

        public async Task<ListResultDto<FormControlDto>> GetFormControlsAsync()
        {
            return await Task.FromResult(
                new ListResultDto<FormControlDto>(
                _formControls.Select(
                    f => new FormControlDto(f.Name, f.DisplayName)
                    ).ToList()
                    ));
        }
    }
}
