import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SelectSearchComponent } from './select-search.component';

describe('SelectSearchComponent', () => {
  let component: SelectSearchComponent;
  let fixture: ComponentFixture<SelectSearchComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [SelectSearchComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(SelectSearchComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
