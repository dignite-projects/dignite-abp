import type { ConnectTenantDomainInput, TenantDomainDto } from './models';
import { RestService, Rest } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class TenantDomainService {
  apiName = 'TenantDomainManagement';
  

  checkCnameRecord = (domainName: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, boolean>({
      method: 'GET',
      url: '/api/tenant-domain-management/tenant-domain/check-cname-record',
      params: { domainName },
    },
    { apiName: this.apiName,...config });
  

  connect = (input: ConnectTenantDomainInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, TenantDomainDto>({
      method: 'POST',
      url: '/api/tenant-domain-management/tenant-domain/connect',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  get = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, TenantDomainDto>({
      method: 'GET',
      url: '/api/tenant-domain-management/tenant-domain',
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
