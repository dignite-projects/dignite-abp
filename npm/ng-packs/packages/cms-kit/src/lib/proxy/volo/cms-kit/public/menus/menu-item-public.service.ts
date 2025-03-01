import { RestService, Rest } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { MenuItemDto } from '../../menus/models';

@Injectable({
  providedIn: 'root',
})
export class MenuItemPublicService {
  apiName = 'CmsKitPublic';
  

  getList = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, MenuItemDto[]>({
      method: 'GET',
      url: '/api/cms-kit-public/menu-items',
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
