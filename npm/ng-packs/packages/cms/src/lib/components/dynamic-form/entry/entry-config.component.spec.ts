import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EntryConfigComponent } from './entry-config.component';

describe('EntryConfigComponent', () => {
  let component: EntryConfigComponent;
  let fixture: ComponentFixture<EntryConfigComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [EntryConfigComponent]
    });
    fixture = TestBed.createComponent(EntryConfigComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
