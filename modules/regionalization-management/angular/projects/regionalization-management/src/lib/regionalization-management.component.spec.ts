import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { RegionalizationManagementComponent } from './components/regionalization-management.component';
import { RegionalizationManagementService } from '@dignite.Abp/regionalization-management';
import { of } from 'rxjs';

describe('RegionalizationManagementComponent', () => {
  let component: RegionalizationManagementComponent;
  let fixture: ComponentFixture<RegionalizationManagementComponent>;
  const mockRegionalizationManagementService = jasmine.createSpyObj('RegionalizationManagementService', {
    sample: of([]),
  });
  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [RegionalizationManagementComponent],
      providers: [
        {
          provide: RegionalizationManagementService,
          useValue: mockRegionalizationManagementService,
        },
      ],
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RegionalizationManagementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
