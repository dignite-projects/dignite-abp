import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateOrEditmenuItemModalComponent } from './create-or-editmenu-item-modal.component';

describe('CreateOrEditmenuItemModalComponent', () => {
  let component: CreateOrEditmenuItemModalComponent;
  let fixture: ComponentFixture<CreateOrEditmenuItemModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CreateOrEditmenuItemModalComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CreateOrEditmenuItemModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
