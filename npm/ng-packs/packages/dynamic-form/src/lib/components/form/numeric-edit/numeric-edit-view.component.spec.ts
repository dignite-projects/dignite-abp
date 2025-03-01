import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NumericEditViewComponent } from './numeric-edit-view.component';

describe('NumericEditViewComponent', () => {
  let component: NumericEditViewComponent;
  let fixture: ComponentFixture<NumericEditViewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [NumericEditViewComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(NumericEditViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
