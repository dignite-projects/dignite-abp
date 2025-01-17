﻿using JetBrains.Annotations;
using System;
using Volo.Abp;

namespace Dignite.Abp.MultiTenancyDomains;

[Serializable]
public class DomainNameAlreadyExistException : BusinessException
{
    public DomainNameAlreadyExistException([NotNull] string name)
    {
        Code = TenantDomainsErrorCodes.DomainNameAlreadyExist;
        WithData(nameof(TenantDomain.DomainName), name);
    }
}
