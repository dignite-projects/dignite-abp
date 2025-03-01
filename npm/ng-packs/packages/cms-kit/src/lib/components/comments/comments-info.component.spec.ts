import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CommentsInfoComponent } from './comments-info.component';

describe('CommentsInfoComponent', () => {
  let component: CommentsInfoComponent;
  let fixture: ComponentFixture<CommentsInfoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CommentsInfoComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CommentsInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
