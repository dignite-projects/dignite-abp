import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MatrixControlComponent } from './matrix-control.component';

describe('MatrixControlComponent', () => {
  let component: MatrixControlComponent;
  let fixture: ComponentFixture<MatrixControlComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [MatrixControlComponent]
    });
    fixture = TestBed.createComponent(MatrixControlComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
