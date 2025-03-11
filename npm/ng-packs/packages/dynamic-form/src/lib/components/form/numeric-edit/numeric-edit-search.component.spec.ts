import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NumericEditSearchComponent } from './numeric-edit-search.component';

describe('NumericEditSearchComponent', () => {
  let component: NumericEditSearchComponent;
  let fixture: ComponentFixture<NumericEditSearchComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [NumericEditSearchComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(NumericEditSearchComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
