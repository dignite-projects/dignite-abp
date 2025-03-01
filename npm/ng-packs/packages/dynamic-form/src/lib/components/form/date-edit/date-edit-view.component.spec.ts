import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DateEditViewComponent } from './date-edit-view.component';

describe('DateEditViewComponent', () => {
  let component: DateEditViewComponent;
  let fixture: ComponentFixture<DateEditViewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [DateEditViewComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DateEditViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
