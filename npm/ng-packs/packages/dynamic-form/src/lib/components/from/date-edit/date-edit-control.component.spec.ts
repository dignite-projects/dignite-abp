import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DateEditControlComponent } from './date-edit-control.component';

describe('DateEditControlComponent', () => {
  let component: DateEditControlComponent;
  let fixture: ComponentFixture<DateEditControlComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DateEditControlComponent]
    });
    fixture = TestBed.createComponent(DateEditControlComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
