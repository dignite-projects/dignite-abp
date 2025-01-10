import { TestBed } from '@angular/core/testing';
import { RegionalizationManagementService } from './services/regionalization-management.service';
import { RestService } from '@abp/ng.core';

describe('RegionalizationManagementService', () => {
  let service: RegionalizationManagementService;
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
    service = TestBed.inject(RegionalizationManagementService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
