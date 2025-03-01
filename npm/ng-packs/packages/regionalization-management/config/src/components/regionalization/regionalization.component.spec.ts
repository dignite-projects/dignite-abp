import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RegionalizationComponent } from './regionalization.component';

describe('RegionalizationComponent', () => {
  let component: RegionalizationComponent;
  let fixture: ComponentFixture<RegionalizationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RegionalizationComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RegionalizationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
