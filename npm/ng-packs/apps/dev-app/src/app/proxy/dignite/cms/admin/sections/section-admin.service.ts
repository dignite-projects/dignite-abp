import type { CreateSectionInput, GetSectionsInput, SectionDto, SectionNameExistsInput, SectionRouteExistsInput, UpdateSectionInput } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class SectionAdminService {
  apiName = 'CmsAdmin';
  

  create = (input: CreateSectionInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, SectionDto>({
      method: 'POST',
      url: '/api/cms-admin/sections',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/cms-admin/sections/${id}`,
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, SectionDto>({
      method: 'GET',
      url: `/api/cms-admin/sections/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getList = (input: GetSectionsInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<SectionDto>>({
      method: 'GET',
      url: '/api/cms-admin/sections',
      params: { siteId: input.siteId, filter: input.filter, isActive: input.isActive, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  nameExists = (input: SectionNameExistsInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, boolean>({
      method: 'GET',
      url: '/api/cms-admin/sections/name-exists',
      params: { siteId: input.siteId, name: input.name },
    },
    { apiName: this.apiName,...config });
  

  routeExists = (input: SectionRouteExistsInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, boolean>({
      method: 'GET',
      url: '/api/cms-admin/sections/route-exists',
      params: { siteId: input.siteId, route: input.route },
    },
    { apiName: this.apiName,...config });
  

  update = (id: string, input: UpdateSectionInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, SectionDto>({
      method: 'PUT',
      url: `/api/cms-admin/sections/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
