import type { CreateEntryInput, CultureExistWithSingleSectionInput, EntryDto, GetEntriesInput, MoveEntryInput, SlugExistsInput, UpdateEntryInput } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { ListResultDto, PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class EntryAdminService {
  apiName = 'CmsAdmin';
  

  activate = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'POST',
      url: `/api/cms-admin/entries/activate/${id}`,
    },
    { apiName: this.apiName,...config });
  

  create = (input: CreateEntryInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, EntryDto>({
      method: 'POST',
      url: '/api/cms-admin/entries',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  cultureExistWithSingleSection = (input: CultureExistWithSingleSectionInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, boolean>({
      method: 'GET',
      url: '/api/cms-admin/entries/culture-exists-with-single-section',
      params: { culture: input.culture, sectionId: input.sectionId, entryTypeId: input.entryTypeId },
    },
    { apiName: this.apiName,...config });
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/cms-admin/entries/${id}`,
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, EntryDto>({
      method: 'GET',
      url: `/api/cms-admin/entries/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getAllVersions = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ListResultDto<EntryDto>>({
      method: 'GET',
      url: `/api/cms-admin/entries/${id}/all-versions`,
    },
    { apiName: this.apiName,...config });
  

  getList = (input: GetEntriesInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<EntryDto>>({
      method: 'GET',
      url: '/api/cms-admin/entries',
      params: { culture: input.culture, sectionId: input.sectionId, entryTypeId: input.entryTypeId, startPublishDate: input.startPublishDate, expiryPublishDate: input.expiryPublishDate, filter: input.filter, status: input.status, creatorId: input.creatorId, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  getListByIds = (sectionId: string, ids: string[], config?: Partial<Rest.Config>) =>
    this.restService.request<any, ListResultDto<EntryDto>>({
      method: 'GET',
      url: '/api/cms-admin/entries/search-by-ids',
      params: { sectionId, ids },
    },
    { apiName: this.apiName,...config });
  

  move = (id: string, input: MoveEntryInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'POST',
      url: `/api/cms-admin/entries/move/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });
  

  slugExists = (input: SlugExistsInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, boolean>({
      method: 'GET',
      url: '/api/cms-admin/entries/slug-exists',
      params: { culture: input.culture, sectionId: input.sectionId, slug: input.slug },
    },
    { apiName: this.apiName,...config });
  

  update = (id: string, input: UpdateEntryInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, EntryDto>({
      method: 'PUT',
      url: '/api/cms-admin/entries',
      params: { id },
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
