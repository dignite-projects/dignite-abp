import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TextEditSearchComponent } from './text-edit-search.component';

describe('TextEditSearchComponent', () => {
  let component: TextEditSearchComponent;
  let fixture: ComponentFixture<TextEditSearchComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [TextEditSearchComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TextEditSearchComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
