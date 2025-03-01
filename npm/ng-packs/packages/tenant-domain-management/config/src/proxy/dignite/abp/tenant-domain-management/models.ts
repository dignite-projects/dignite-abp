
export interface ConnectTenantDomainInput {
  domainName: string;
}

export interface TenantDomainDto {
  domainName?: string;
  isValid: boolean;
  proxyAddress?: string;
  tenantId?: string;
}
