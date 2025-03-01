import { TestBed } from '@angular/core/testing';

import { TenantDomainManagementService } from './tenant-domain-management.service';

describe('TenantDomainManagementService', () => {
  let service: TenantDomainManagementService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TenantDomainManagementService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
