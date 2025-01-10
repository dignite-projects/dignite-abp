import { Injectable } from '@angular/core';
import { RestService } from '@abp/ng.core';

@Injectable({
  providedIn: 'root',
})
export class RegionalizationManagementService {
  apiName = 'RegionalizationManagement';

  constructor(private restService: RestService) {}

  sample() {
    return this.restService.request<void, any>(
      { method: 'GET', url: '/api/RegionalizationManagement/sample' },
      { apiName: this.apiName }
    );
  }
}
