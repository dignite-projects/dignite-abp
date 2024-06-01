import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SelectConfigComponent } from './select-config.component';

describe('SelectConfigComponent', () => {
  let component: SelectConfigComponent;
  let fixture: ComponentFixture<SelectConfigComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SelectConfigComponent]
    });
    fixture = TestBed.createComponent(SelectConfigComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
