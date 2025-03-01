import type { TenantDomainDto, UpdateTenantDomainInput } from './models';
import { RestService, Rest } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class TenantDomainService {
  apiName = 'MultiTenancyDomains';
  

  domainNameExists = (domainName: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, boolean>({
      method: 'GET',
      url: '/api/multi-tenancy-domains/tenant-domain/domain-name-exists',
      params: { domainName },
    },
    { apiName: this.apiName,...config });
  

  findByCurrentTenant = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, TenantDomainDto>({
      method: 'GET',
      url: '/api/multi-tenancy-domains/tenant-domain/find-by-current-tenant',
    },
    { apiName: this.apiName,...config });
  

  findByDomainName = (domainName: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, TenantDomainDto>({
      method: 'GET',
      url: '/api/multi-tenancy-domains/tenant-domain/find-by-domain-name',
      params: { domainName },
    },
    { apiName: this.apiName,...config });
  

  update = (input: UpdateTenantDomainInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, TenantDomainDto>({
      method: 'PUT',
      url: '/api/multi-tenancy-domains/tenant-domain',
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
