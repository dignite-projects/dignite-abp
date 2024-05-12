import type { CreateFieldInput, FieldDto, GetFieldsInput, UpdateFieldInput } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class FieldAdminService {
  apiName = 'CmsAdmin';
  

  create = (input: CreateFieldInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, FieldDto>({
      method: 'POST',
      url: '/api/cms-admin/fields',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/cms-admin/fields/${id}`,
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, FieldDto>({
      method: 'GET',
      url: `/api/cms-admin/fields/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getList = (input: GetFieldsInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<FieldDto>>({
      method: 'GET',
      url: '/api/cms-admin/fields',
      params: { filter: input.filter, groupId: input.groupId, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  nameExists = (name: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, boolean>({
      method: 'GET',
      url: `/api/cms-admin/fields/name-exists/${name}`,
    },
    { apiName: this.apiName,...config });
  

  update = (id: string, input: UpdateFieldInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, FieldDto>({
      method: 'PUT',
      url: `/api/cms-admin/fields/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
