import type { CreateSiteInput, GetSitesInput, SiteDto, UpdateSiteInput } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class SiteAdminService {
  apiName = 'CmsAdmin';
  

  create = (input: CreateSiteInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, SiteDto>({
      method: 'POST',
      url: '/api/cms-admin/sites',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/cms-admin/sites/${id}`,
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, SiteDto>({
      method: 'GET',
      url: `/api/cms-admin/sites/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getList = (input: GetSitesInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<SiteDto>>({
      method: 'GET',
      url: '/api/cms-admin/sites',
      params: { filter: input.filter, isActive: input.isActive },
    },
    { apiName: this.apiName,...config });
  

  hostExists = (name: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, boolean>({
      method: 'GET',
      url: `/api/cms-admin/sites/host-exists/${name}`,
    },
    { apiName: this.apiName,...config });
  

  nameExists = (name: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, boolean>({
      method: 'GET',
      url: `/api/cms-admin/sites/name-exists/${name}`,
    },
    { apiName: this.apiName,...config });
  

  update = (id: string, input: UpdateSiteInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, SiteDto>({
      method: 'PUT',
      url: `/api/cms-admin/sites/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
