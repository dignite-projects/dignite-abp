import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DynamicConfigComponent } from './dynamic-config.component';

describe('DynamicConfigComponent', () => {
  let component: DynamicConfigComponent;
  let fixture: ComponentFixture<DynamicConfigComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [DynamicConfigComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DynamicConfigComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
