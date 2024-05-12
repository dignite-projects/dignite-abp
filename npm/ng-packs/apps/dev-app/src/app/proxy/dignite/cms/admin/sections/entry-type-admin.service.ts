import type { CreateEntryTypeInput, EntryTypeDto, EntryTypeNameExistsInput, UpdateEntryTypeInput } from './models';
import { RestService, Rest } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class EntryTypeAdminService {
  apiName = 'CmsAdmin';
  

  create = (input: CreateEntryTypeInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, EntryTypeDto>({
      method: 'POST',
      url: '/api/cms-admin/entry-types',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/cms-admin/entry-types/${id}`,
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, EntryTypeDto>({
      method: 'GET',
      url: `/api/cms-admin/entry-types/${id}`,
    },
    { apiName: this.apiName,...config });
  

  nameExists = (input: EntryTypeNameExistsInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, boolean>({
      method: 'GET',
      url: '/api/cms-admin/entry-types/name-exists',
      params: { sectionId: input.sectionId, name: input.name },
    },
    { apiName: this.apiName,...config });
  

  update = (id: string, input: UpdateEntryTypeInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, EntryTypeDto>({
      method: 'PUT',
      url: `/api/cms-admin/entry-types/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
