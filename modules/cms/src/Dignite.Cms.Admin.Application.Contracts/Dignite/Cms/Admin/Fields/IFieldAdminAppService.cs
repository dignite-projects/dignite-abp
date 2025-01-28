using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Dignite.Cms.Admin.Fields
{
    public interface IFieldAdminAppService
    : ICrudAppService<
        FieldDto,
        Guid,
        GetFieldsInput,
        CreateFieldInput,
        UpdateFieldInput>
    {
        Task<bool> NameExistsAsync(string name);
    }
}
