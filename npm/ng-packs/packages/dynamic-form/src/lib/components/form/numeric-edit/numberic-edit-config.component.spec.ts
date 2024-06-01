import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NumbericEditConfigComponent } from './numberic-edit-config.component';

describe('NumbericEditConfigComponent', () => {
  let component: NumbericEditConfigComponent;
  let fixture: ComponentFixture<NumbericEditConfigComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [NumbericEditConfigComponent]
    });
    fixture = TestBed.createComponent(NumbericEditConfigComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
