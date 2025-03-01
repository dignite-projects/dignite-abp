import type { CreateShortenedUrlDto, GetShortenedUrlListInput, ShortenedUrlDto, UpdateShortenedUrlDto } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class UrlShortingAdminService {
  apiName = 'CmsKitAdmin';
  

  create = (input: CreateShortenedUrlDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ShortenedUrlDto>({
      method: 'POST',
      url: '/api/cms-kit-admin/url-shorting',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/cms-kit-admin/url-shorting/${id}`,
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ShortenedUrlDto>({
      method: 'GET',
      url: `/api/cms-kit-admin/url-shorting/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getList = (input: GetShortenedUrlListInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<ShortenedUrlDto>>({
      method: 'GET',
      url: '/api/cms-kit-admin/url-shorting',
      params: { shortenedUrlFilter: input.shortenedUrlFilter, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  update = (id: string, input: UpdateShortenedUrlDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ShortenedUrlDto>({
      method: 'PUT',
      url: `/api/cms-kit-admin/url-shorting/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
