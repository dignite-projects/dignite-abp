import type { CmsKitContactSettingDto, UpdateCmsKitContactSettingDto } from './models';
import { RestService, Rest } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class ContactSettingService {
  apiName = 'CmsKitAdmin';
  

  get = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, CmsKitContactSettingDto>({
      method: 'GET',
      url: '/api/cms-kit-admin/contact/settings',
    },
    { apiName: this.apiName,...config });
  

  update = (input: UpdateCmsKitContactSettingDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'POST',
      url: '/api/cms-kit-admin/contact/settings',
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
