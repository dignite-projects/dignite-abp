import type { RegionalizationDto, UpdateRegionalizationInput } from './models';
import { RestService, Rest } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class RegionalizationService {
  apiName = 'AbpRegionalizationManagement';
  

  get = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, RegionalizationDto>({
      method: 'GET',
      url: '/api/regionalization-management/regionalization',
    },
    { apiName: this.apiName,...config });
  

  update = (input: UpdateRegionalizationInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, RegionalizationDto>({
      method: 'POST',
      url: '/api/regionalization-management/regionalization',
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
