import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CkedComponent } from './cked.component';

describe('CkedComponent', () => {
  let component: CkedComponent;
  let fixture: ComponentFixture<CkedComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CkedComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CkedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
