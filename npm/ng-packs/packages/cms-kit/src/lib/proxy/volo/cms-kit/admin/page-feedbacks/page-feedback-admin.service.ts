import type { GetPageFeedbackListInput, PageFeedbackDto, PageFeedbackSettingDto, UpdatePageFeedbackDto, UpdatePageFeedbackSettingsInput } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class PageFeedbackAdminService {
  apiName = 'CmsKitAdmin';
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/cms-kit-admin/page-feedback/${id}`,
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PageFeedbackDto>({
      method: 'GET',
      url: `/api/cms-kit-admin/page-feedback/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getEntityTypes = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, string[]>({
      method: 'GET',
      url: '/api/cms-kit-admin/page-feedback/entity-types',
    },
    { apiName: this.apiName,...config });
  

  getList = (input: GetPageFeedbackListInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<PageFeedbackDto>>({
      method: 'GET',
      url: '/api/cms-kit-admin/page-feedback',
      params: { entityType: input.entityType, entityId: input.entityId, isHandled: input.isHandled, isUseful: input.isUseful, url: input.url, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  getSettings = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, PageFeedbackSettingDto[]>({
      method: 'GET',
      url: '/api/cms-kit-admin/page-feedback/settings',
    },
    { apiName: this.apiName,...config });
  

  update = (id: string, dto: UpdatePageFeedbackDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PageFeedbackDto>({
      method: 'PUT',
      url: `/api/cms-kit-admin/page-feedback/${id}`,
      body: dto,
    },
    { apiName: this.apiName,...config });
  

  updateSettings = (input: UpdatePageFeedbackSettingsInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'PUT',
      url: '/api/cms-kit-admin/page-feedback/settings',
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
