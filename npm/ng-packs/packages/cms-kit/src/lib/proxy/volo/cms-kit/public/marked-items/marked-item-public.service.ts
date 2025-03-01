import type { MarkedItemWithToggleDto } from './models';
import { RestService, Rest } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class MarkedItemPublicService {
  apiName = 'CmsKitPublic';
  

  getForUser = (entityType: string, entityId: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, MarkedItemWithToggleDto>({
      method: 'GET',
      url: `/api/cms-kit-public/marked-items/${entityType}/${entityId}`,
    },
    { apiName: this.apiName,...config });
  

  toggle = (entityType: string, entityId: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, boolean>({
      method: 'PUT',
      url: `/api/cms-kit-public/marked-items/${entityType}/${entityId}`,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
