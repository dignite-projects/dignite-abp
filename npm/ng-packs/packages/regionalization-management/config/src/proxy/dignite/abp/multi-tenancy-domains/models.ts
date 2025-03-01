import type { EntityDto } from '@abp/ng.core';

export interface TenantDomainDto extends EntityDto<string> {
  domainName?: string;
  tenantId?: string;
}

export interface UpdateTenantDomainInput {
  domainName: string;
}
