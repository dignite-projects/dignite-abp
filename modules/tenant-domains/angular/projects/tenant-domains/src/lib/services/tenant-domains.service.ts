import { Injectable } from '@angular/core';
import { RestService } from '@abp/ng.core';

@Injectable({
  providedIn: 'root',
})
export class TenantDomainsService {
  apiName = 'TenantDomains';

  constructor(private restService: RestService) {}

  sample() {
    return this.restService.request<void, any>(
      { method: 'GET', url: '/api/TenantDomains/sample' },
      { apiName: this.apiName }
    );
  }
}
