import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateOrEditSectionsModalComponent } from './create-or-edit-sections-modal.component';

describe('CreateOrEditSectionsModalComponent', () => {
  let component: CreateOrEditSectionsModalComponent;
  let fixture: ComponentFixture<CreateOrEditSectionsModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CreateOrEditSectionsModalComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CreateOrEditSectionsModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
