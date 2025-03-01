import type { BrandSettingsDto, UpdateBrandSettingsInput } from './models';
import { RestService, Rest } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class BrandSettingsAdminService {
  apiName = 'TravelyAdmin';
  

  get = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, BrandSettingsDto>({
      method: 'GET',
      url: '/api/travely-admin/brand-settings',
    },
    { apiName: this.apiName,...config });
  

  update = (input: UpdateBrandSettingsInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'POST',
      url: '/api/travely-admin/brand-settings',
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
