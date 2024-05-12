import type { FormControlDto } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { ListResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class FormAdminService {
  apiName = 'CmsAdmin';
  

  getFormControls = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, ListResultDto<FormControlDto>>({
      method: 'GET',
      url: '/api/cms-admin/dynamic-forms/forms',
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
