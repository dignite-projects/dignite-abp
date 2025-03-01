import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MatrixConfigComponent } from './matrix-config.component';

describe('MatrixConfigComponent', () => {
  let component: MatrixConfigComponent;
  let fixture: ComponentFixture<MatrixConfigComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [MatrixConfigComponent]
    });
    fixture = TestBed.createComponent(MatrixConfigComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
