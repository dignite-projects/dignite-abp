import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DynamicComponent } from './dynamic.component';

describe('DynamicComponent', () => {
  let component: DynamicComponent;
  let fixture: ComponentFixture<DynamicComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DynamicComponent]
    });
    fixture = TestBed.createComponent(DynamicComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
