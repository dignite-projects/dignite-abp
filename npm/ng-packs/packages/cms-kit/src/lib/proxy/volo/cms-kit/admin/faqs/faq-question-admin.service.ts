import type { CreateFaqQuestionDto, FaqQuestionDto, FaqQuestionListFilterDto, UpdateFaqQuestionDto } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class FaqQuestionAdminService {
  apiName = 'CmsKitAdmin';
  

  create = (input: CreateFaqQuestionDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, FaqQuestionDto>({
      method: 'POST',
      url: '/api/cms-kit-admin/faq-question',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: '/api/cms-kit-admin/faq-question',
      params: { id },
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, FaqQuestionDto>({
      method: 'GET',
      url: `/api/cms-kit-admin/faq-question/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getList = (input: FaqQuestionListFilterDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<FaqQuestionDto>>({
      method: 'GET',
      url: '/api/cms-kit-admin/faq-question',
      params: { sectionId: input.sectionId, filter: input.filter, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  update = (id: string, input: UpdateFaqQuestionDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, FaqQuestionDto>({
      method: 'PUT',
      url: '/api/cms-kit-admin/faq-question',
      params: { id },
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
