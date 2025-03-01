import type { CommentDto, CommentWithDetailsDto, CreateCommentInput, UpdateCommentInput } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { ListResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class CommentPublicService {
  apiName = 'CmsKitPublic';
  

  create = (entityType: string, entityId: string, input: CreateCommentInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, CommentDto>({
      method: 'POST',
      url: `/api/cms-kit-public/comments/${entityType}/${entityId}`,
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/cms-kit-public/comments/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getList = (entityType: string, entityId: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ListResultDto<CommentWithDetailsDto>>({
      method: 'GET',
      url: `/api/cms-kit-public/comments/${entityType}/${entityId}`,
    },
    { apiName: this.apiName,...config });
  

  update = (id: string, input: UpdateCommentInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, CommentDto>({
      method: 'PUT',
      url: `/api/cms-kit-public/comments/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
