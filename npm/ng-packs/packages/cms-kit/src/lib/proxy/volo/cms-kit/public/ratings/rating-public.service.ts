import type { CreateUpdateRatingInput, RatingDto, RatingWithStarCountDto } from './models';
import { RestService, Rest } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class RatingPublicService {
  apiName = 'CmsKitPublic';
  

  create = (entityType: string, entityId: string, input: CreateUpdateRatingInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, RatingDto>({
      method: 'PUT',
      url: `/api/cms-kit-public/ratings/${entityType}/${entityId}`,
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (entityType: string, entityId: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/cms-kit-public/ratings/${entityType}/${entityId}`,
    },
    { apiName: this.apiName,...config });
  

  getGroupedStarCounts = (entityType: string, entityId: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, RatingWithStarCountDto[]>({
      method: 'GET',
      url: `/api/cms-kit-public/ratings/${entityType}/${entityId}`,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
