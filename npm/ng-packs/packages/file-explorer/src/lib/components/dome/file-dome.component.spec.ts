import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FileDomeComponent } from './file-dome.component';

describe('FileDomeComponent', () => {
  let component: FileDomeComponent;
  let fixture: ComponentFixture<FileDomeComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [FileDomeComponent]
    });
    fixture = TestBed.createComponent(FileDomeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
