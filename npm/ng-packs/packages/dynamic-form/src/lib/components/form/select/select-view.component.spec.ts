import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SelectViewComponent } from './select-view.component';

describe('SelectViewComponent', () => {
  let component: SelectViewComponent;
  let fixture: ComponentFixture<SelectViewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [SelectViewComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(SelectViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
