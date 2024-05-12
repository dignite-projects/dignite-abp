import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateFieldComponent } from './create-field.component';

describe('CreateFieldComponent', () => {
  let component: CreateFieldComponent;
  let fixture: ComponentFixture<CreateFieldComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CreateFieldComponent]
    });
    fixture = TestBed.createComponent(CreateFieldComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
