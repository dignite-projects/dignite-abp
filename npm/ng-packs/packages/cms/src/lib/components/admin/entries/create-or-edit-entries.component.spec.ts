import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateOrEditEntriesComponent } from './create-or-edit-entries.component';

describe('CreateOrEditEntriesComponent', () => {
  let component: CreateOrEditEntriesComponent;
  let fixture: ComponentFixture<CreateOrEditEntriesComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CreateOrEditEntriesComponent]
    });
    fixture = TestBed.createComponent(CreateOrEditEntriesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
