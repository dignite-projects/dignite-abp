import type { CreateFaqSectionDto, FaqGroupInfoDto, FaqSectionDto, FaqSectionListFilterDto, FaqSectionWithQuestionCountDto, UpdateFaqSectionDto } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class FaqSectionAdminService {
  apiName = 'CmsKitAdmin';
  

  create = (input: CreateFaqSectionDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, FaqSectionDto>({
      method: 'POST',
      url: '/api/cms-kit-admin/faq-section',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: '/api/cms-kit-admin/faq-section',
      params: { id },
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, FaqSectionDto>({
      method: 'GET',
      url: `/api/cms-kit-admin/faq-section/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getGroups = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, Record<string, FaqGroupInfoDto>>({
      method: 'GET',
      url: '/api/cms-kit-admin/faq-section/groups',
    },
    { apiName: this.apiName,...config });
  

  getList = (input: FaqSectionListFilterDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<FaqSectionWithQuestionCountDto>>({
      method: 'GET',
      url: '/api/cms-kit-admin/faq-section',
      params: { filter: input.filter, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  update = (id: string, input: UpdateFaqSectionDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, FaqSectionDto>({
      method: 'PUT',
      url: '/api/cms-kit-admin/faq-section',
      params: { id },
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
