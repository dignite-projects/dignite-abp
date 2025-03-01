import type { SiteLanguageSettingsDto, UpdateSiteLanguageSettingsInput } from './models';
import { RestService, Rest } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class SiteLanguageSettingsAdminService {
  apiName = 'TravelyAdmin';
  

  get = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, SiteLanguageSettingsDto>({
      method: 'GET',
      url: '/api/travely-admin/site-language-settings',
    },
    { apiName: this.apiName,...config });
  

  update = (input: UpdateSiteLanguageSettingsInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'POST',
      url: '/api/travely-admin/site-language-settings',
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
