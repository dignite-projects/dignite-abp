import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FileModalTreeComponent } from './file-modal-tree.component';

describe('FileModalTreeComponent', () => {
  let component: FileModalTreeComponent;
  let fixture: ComponentFixture<FileModalTreeComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [FileModalTreeComponent]
    });
    fixture = TestBed.createComponent(FileModalTreeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
