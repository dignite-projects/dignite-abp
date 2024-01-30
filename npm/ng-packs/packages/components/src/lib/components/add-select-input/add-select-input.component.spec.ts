import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddSelectInputComponent } from './add-select-input.component';

describe('AddSelectInputComponent', () => {
  let component: AddSelectInputComponent;
  let fixture: ComponentFixture<AddSelectInputComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AddSelectInputComponent]
    });
    fixture = TestBed.createComponent(AddSelectInputComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
