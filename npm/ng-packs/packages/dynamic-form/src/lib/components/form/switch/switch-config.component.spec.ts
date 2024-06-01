import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SwitchConfigComponent } from './switch-config.component';

describe('SwitchConfigComponent', () => {
  let component: SwitchConfigComponent;
  let fixture: ComponentFixture<SwitchConfigComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SwitchConfigComponent]
    });
    fixture = TestBed.createComponent(SwitchConfigComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
