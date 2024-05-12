import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateOrEditComponent } from './create-or-edit.component';

describe('CreateOrEditComponent', () => {
  let component: CreateOrEditComponent;
  let fixture: ComponentFixture<CreateOrEditComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CreateOrEditComponent]
    });
    fixture = TestBed.createComponent(CreateOrEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
