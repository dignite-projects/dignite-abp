import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DateEditConfigComponent } from './date-edit-config.component';

describe('DateEditConfigComponent', () => {
  let component: DateEditConfigComponent;
  let fixture: ComponentFixture<DateEditConfigComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DateEditConfigComponent]
    });
    fixture = TestBed.createComponent(DateEditConfigComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
