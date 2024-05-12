import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EntryControlComponent } from './entry-control.component';

describe('EntryControlComponent', () => {
  let component: EntryControlComponent;
  let fixture: ComponentFixture<EntryControlComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [EntryControlComponent]
    });
    fixture = TestBed.createComponent(EntryControlComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
