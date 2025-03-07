import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SwitchSearchComponent } from './switch-search.component';

describe('SwitchSearchComponent', () => {
  let component: SwitchSearchComponent;
  let fixture: ComponentFixture<SwitchSearchComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [SwitchSearchComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(SwitchSearchComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
