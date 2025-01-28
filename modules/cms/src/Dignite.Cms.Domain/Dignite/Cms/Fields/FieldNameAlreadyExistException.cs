using JetBrains.Annotations;
using System;
using Volo.Abp;

namespace Dignite.Cms.Fields
{
    [Serializable]
    public class FieldNameAlreadyExistException : BusinessException
    {
        public FieldNameAlreadyExistException([NotNull]string name)
        {
            Code = CmsErrorCodes.Fields.NameAlreadyExist;
            WithData(nameof(Field.Name), name);
        }
    }
}
