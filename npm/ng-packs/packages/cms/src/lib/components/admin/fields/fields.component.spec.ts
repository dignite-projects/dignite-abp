import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FieldsComponent } from './fields.component';

describe('FieldsComponent', () => {
  let component: FieldsComponent;
  let fixture: ComponentFixture<FieldsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [FieldsComponent]
    });
    fixture = TestBed.createComponent(FieldsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
