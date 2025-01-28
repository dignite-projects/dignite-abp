using System;
using Volo.Abp.Application.Services;

namespace Dignite.Cms.Admin.Fields
{
    public interface IFieldGroupAdminAppService : ICrudAppService<FieldGroupDto, Guid, GetFieldGroupsInput, CreateOrUpdateFieldGroupInput>
    {
    }
}
