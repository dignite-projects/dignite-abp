import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateOrEditFieldComponent } from './create-or-edit-field.component';

describe('CreateOrEditFieldComponent', () => {
  let component: CreateOrEditFieldComponent;
  let fixture: ComponentFixture<CreateOrEditFieldComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CreateOrEditFieldComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CreateOrEditFieldComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
