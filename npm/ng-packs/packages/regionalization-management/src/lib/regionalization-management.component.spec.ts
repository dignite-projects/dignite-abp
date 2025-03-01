import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RegionalizationManagementComponent } from './regionalization-management.component';

describe('RegionalizationManagementComponent', () => {
  let component: RegionalizationManagementComponent;
  let fixture: ComponentFixture<RegionalizationManagementComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RegionalizationManagementComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RegionalizationManagementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
