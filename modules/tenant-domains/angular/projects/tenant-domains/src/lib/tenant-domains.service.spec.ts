import { TestBed } from '@angular/core/testing';
import { TenantDomainsService } from './services/tenant-domains.service';
import { RestService } from '@abp/ng.core';

describe('TenantDomainsService', () => {
  let service: TenantDomainsService;
  const mockRestService = jasmine.createSpyObj('RestService', ['request']);
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        {
          provide: RestService,
          useValue: mockRestService,
        },
      ],
    });
    service = TestBed.inject(TenantDomainsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
