import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TextEditComponent } from './text-edit.component';

describe('TextEditComponent', () => {
  let component: TextEditComponent;
  let fixture: ComponentFixture<TextEditComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [TextEditComponent]
    });
    fixture = TestBed.createComponent(TextEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
