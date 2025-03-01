import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SwitchViewComponent } from './switch-view.component';

describe('SwitchViewComponent', () => {
  let component: SwitchViewComponent;
  let fixture: ComponentFixture<SwitchViewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [SwitchViewComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(SwitchViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
