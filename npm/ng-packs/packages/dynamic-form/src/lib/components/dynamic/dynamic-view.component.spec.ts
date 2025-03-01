import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DynamicViewComponent } from './dynamic-view.component';

describe('DynamicViewComponent', () => {
  let component: DynamicViewComponent;
  let fixture: ComponentFixture<DynamicViewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [DynamicViewComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DynamicViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
