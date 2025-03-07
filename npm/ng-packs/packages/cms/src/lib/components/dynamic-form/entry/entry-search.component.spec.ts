import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EntrySearchComponent } from './entry-search.component';

describe('EntrySearchComponent', () => {
  let component: EntrySearchComponent;
  let fixture: ComponentFixture<EntrySearchComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EntrySearchComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(EntrySearchComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
