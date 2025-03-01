import { TestBed } from '@angular/core/testing';

import { RegionalizationManagementService } from './regionalization-management.service';

describe('RegionalizationManagementService', () => {
  let service: RegionalizationManagementService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(RegionalizationManagementService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
