import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NumbericEditControlComponent } from './numberic-edit-control.component';

describe('NumbericEditControlComponent', () => {
  let component: NumbericEditControlComponent;
  let fixture: ComponentFixture<NumbericEditControlComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [NumbericEditControlComponent]
    });
    fixture = TestBed.createComponent(NumbericEditControlComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
