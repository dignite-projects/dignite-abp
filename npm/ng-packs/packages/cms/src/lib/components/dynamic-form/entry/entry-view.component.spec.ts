import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EntryViewComponent } from './entry-view.component';

describe('EntryViewComponent', () => {
  let component: EntryViewComponent;
  let fixture: ComponentFixture<EntryViewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EntryViewComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(EntryViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
