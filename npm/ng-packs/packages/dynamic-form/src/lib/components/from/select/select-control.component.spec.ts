import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SelectControlComponent } from './select-control.component';

describe('SelectControlComponent', () => {
  let component: SelectControlComponent;
  let fixture: ComponentFixture<SelectControlComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SelectControlComponent]
    });
    fixture = TestBed.createComponent(SelectControlComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
