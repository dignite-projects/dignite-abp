import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FileExplorerControlComponent } from './file-explorer-control.component';

describe('FileExplorerControlComponent', () => {
  let component: FileExplorerControlComponent;
  let fixture: ComponentFixture<FileExplorerControlComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [FileExplorerControlComponent]
    });
    fixture = TestBed.createComponent(FileExplorerControlComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
