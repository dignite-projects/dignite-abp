import { TestBed } from '@angular/core/testing';

import { LocationBackService } from './location-back.service';

describe('LocationBackService', () => {
  let service: LocationBackService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(LocationBackService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
