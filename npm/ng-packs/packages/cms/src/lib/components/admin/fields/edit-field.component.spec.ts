import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditFieldComponent } from './edit-field.component';

describe('EditFieldComponent', () => {
  let component: EditFieldComponent;
  let fixture: ComponentFixture<EditFieldComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [EditFieldComponent]
    });
    fixture = TestBed.createComponent(EditFieldComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
