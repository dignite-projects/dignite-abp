import type { CreateOrUpdateFieldGroupInput, FieldGroupDto, GetFieldGroupsInput } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class FieldGroupAdminService {
  apiName = 'CmsAdmin';
  

  create = (input: CreateOrUpdateFieldGroupInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, FieldGroupDto>({
      method: 'POST',
      url: '/api/cms-admin/field-groups',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/cms-admin/field-groups/${id}`,
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, FieldGroupDto>({
      method: 'GET',
      url: `/api/cms-admin/field-groups/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getList = (input: GetFieldGroupsInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<FieldGroupDto>>({
      method: 'GET',
      url: '/api/cms-admin/field-groups',
      params: { input },
    },
    { apiName: this.apiName,...config });
  

  update = (id: string, input: CreateOrUpdateFieldGroupInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, FieldGroupDto>({
      method: 'PUT',
      url: `/api/cms-admin/field-groups/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
